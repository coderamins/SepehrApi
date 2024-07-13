using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sepehr.Infrastructure.Persistence.Data
{
    /// <inheritdoc />
    public partial class migration_trigger2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_OrderDetailLog]    Script Date: 2/20/2024 10:26:29 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_OrderDetailLog]
               ON  [sepdb].[OrderDetails]
               AFTER INSERT,DELETE,UPDATE
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	select * into #t from inserted
            	union (select * from deleted);

            	DECLARE @event_type int
            	IF EXISTS(SELECT * FROM inserted)
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 2
            	ELSE
            		SELECT @event_type = 1
            	ELSE
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 3
            	  ELSE
            		--no rows affected - cannot determine event
            		SELECT @event_type = 4

            	insert into seplog.OrderDetailLogs
            		([MainId], [OrderId], [RowId], [ProductId], [ProductBrandId], [WarehouseId], [ProximateAmount],
            		[PackageNumber], [NumberInPackage], [Price], [SellerCompanyRow], 
            		[PurchaserCustomerId], [PurchasePrice], [PurchaseInvoiceTypeId], [PurchaseSettlementDate], 
            		[Description], [AlternativeProductId], [AlternativeProductAmount], [AlternativeProductPrice],
            		[IsActive], [LogTypeId],LogCreateDate
            		)
            	select top 1 
            		 Id,OrderId,RowId,ProductId,ProductBrandId,WarehouseId,ProximateAmount,PackageNumber,
            		 NumberInPackage,Price,SellerCompanyRow,PurchaserCustomerId,PurchasePrice,
            		 PurchaseInvoiceTypeId,PurchaseSettlementDate,Description,AlternativeProductId,
            		 AlternativeProductAmount,AlternativeProductPrice,IsActive,@event_type,GetDate()
            	from #t;

            	drop table #t;
            	--select * from  [seplog].[OrderDetailLogs]

            END");

            //----------Order Payment Log---------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_OrderPaymentLog]    Script Date: 2/20/2024 10:31:40 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_OrderPaymentLog]
               ON  [sepdb].[OrderPayments]
               AFTER INSERT,DELETE,UPDATE
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	select * into #t from inserted
            	union (select * from deleted);

            	DECLARE @event_type int
            	IF EXISTS(SELECT * FROM inserted)
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 2
            	ELSE
            		SELECT @event_type = 1
            	ELSE
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 3
            	  ELSE
            		--no rows affected - cannot determine event
            		SELECT @event_type = 4

            	insert into seplog.OrderPaymentLogs
            		([Id], [MainId],[OrderId], [Amount], [PaymentDate], [DaysAfterExit], [PaymentType], [IsActive],[LogTypeId])
            	select top 1 
            		 (select NEWID()),Id,OrderId,Amount,PaymentDate,DaysAfterExit,PaymentType,IsActive,@event_type
            	from #t;

            	drop table #t;
            	--select * from  OrderPayments

            END; disable trigger [TR_OrderPaymentLog] on [sepdb].[OrderPayments];");

            //---------Order Log-----------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_OrderLog]    Script Date: 2/20/2024 10:33:16 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_OrderLog]
               ON  [sepdb].[Orders]
               AFTER INSERT,DELETE,UPDATE
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	-- possible error occurs here...

            			select * into #order from inserted
            			union (select * from deleted);

            			DECLARE @event_type int
            			IF EXISTS(SELECT * FROM inserted)
            			  IF EXISTS(SELECT * FROM deleted)
            				SELECT @event_type = 2
            			ELSE
            				SELECT @event_type = 1
            			ELSE
            			  IF EXISTS(SELECT * FROM deleted)
            				SELECT @event_type = 3
            			  ELSE
            				--no rows affected - cannot determine event
            				SELECT @event_type = 4

            			insert into seplog.OrderLogs
            				(Id,MainId, OrderCode,CustomerId,TotalAmount,ConfirmedStatus,ExitType,
            				OrderSendTypeId,FarePaymentTypeId,InvoiceTypeId,ApprovedDate,
            				OrderStatusId,Barcode,Description,CreatedBy,
            				Created,LastModifiedBy,LastModified,IsActive,LogTypeId,LogCreateDate)
            			select top 1 
            				(select NEWID()),Id,OrderCode,CustomerId,TotalAmount,ConfirmedStatus,ExitType,
            				OrderSendTypeId,FarePaymentTypeId,InvoiceTypeId,ApprovedDate,
            				OrderStatusId,Barcode,Description,CreatedBy,
            				Created,LastModifiedBy,LastModified,IsActive,@event_type,GetDate()
            			from #order;

            			drop table #order;
            	END");

            //-------------Create Warehouse Inventory----------------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_CreateWarehouseInventory]    Script Date: 2/20/2024 10:35:05 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_CreateWarehouseInventory]
               ON  [sepdb].[ProductBrands] 
               AFTER INSERT
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	declare @warehouseId int

            	DECLARE db_cursor CURSOR
            		FOR SELECT DISTINCT Id from sepdb.Warehouses as w where w.WarehouseTypeId!=5
            			--and not exists (select Id from sepdb.ProductInventories 
            			--				  where ProductBrandId=(select Id from inserted) and WarehouseId=Id)  

            	OPEN db_cursor
            	FETCH NEXT FROM db_cursor
            	INTO @warehouseId

            	WHILE @@FETCH_STATUS = 0

            	BEGIN

            		insert into sepdb.ProductInventories 
            			([ProductBrandId], [Thickness], [WarehouseId], [ApproximateInventory], [FloorInventory], 
            			[MaxInventory], [MinInventory], [OrderPoint], [CreatedBy], [Created],[IsActive])
            			select
            			Id,0,@warehouseId,0,0,0,0,0,'882d46b7-9df8-49bd-a414-2074bcfbcf35',GETDATE(),1
            			from inserted

            	FETCH NEXT FROM db_cursor
            	INTO @warehouseId

            	END

            	CLOSE db_cursor
            	DEALLOCATE db_cursor
            END");


            //----------------Product Brand Event---------------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_ProductBrandEvents]    Script Date: 2/20/2024 10:36:21 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_ProductBrandEvents]
               ON  [sepdb].[ProductBrands]
               AFTER INSERT,DELETE,UPDATE
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	select * into #t from inserted
            	union (select * from deleted);

            	DECLARE @event_type int
            	IF EXISTS(SELECT * FROM inserted)
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 2
            	ELSE
            		SELECT @event_type = 1
            	ELSE
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 3
            	  ELSE
            		--no rows affected - cannot determine event
            		SELECT @event_type = 4

            	insert into [seplog].[OrderServiceLogs]
            		(MainId, [ProductId], [BrandId], [MainId], [IsActive], [LogTypeId])
            	select top 1 
            		 Id,[ProductId], [BrandId], [MainId], [IsActive], @event_type
            	from #t;

            	drop table #t;
            	--select * from  OrderPayments

            END; disable trigger [TR_ProductBrandEvents] on [sepdb].[ProductBrands];");

            //------------Product Details Event-------------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_ProductDetailEvents]    Script Date: 2/20/2024 10:41:06 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_ProductDetailEvents]
               ON  [sepdb].[ProductDetails]
               AFTER INSERT,DELETE,UPDATE
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	select * into #t from inserted
            	union (select * from deleted);

            	DECLARE @event_type int
            	IF EXISTS(SELECT * FROM inserted)
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 2
            	ELSE
            		SELECT @event_type = 1
            	ELSE
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 3
            	  ELSE
            		--no rows affected - cannot determine event
            		SELECT @event_type = 4

            	insert into [seplog].[ProductDetailLogs]
            		(MainId, [Size], [Standard], [ProductState], [ProductId], [IsActive], [LogTypeId])
            	select top 1 
            		 Id,[Size], [Standard], [ProductState], [ProductId], [IsActive], @event_type
            	from #t;

            	drop table #t;
            	--select * from  [seplog].[ProductDetailLogs]OrderPayments

            END");

            //------------Product Inventory Events-------------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[ProductInventoryEvents]    Script Date: 2/20/2024 10:43:04 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            CREATE TRIGGER [sepdb].[ProductInventoryEvents]
            ON  [sepdb].[ProductInventories]
            AFTER INSERT,DELETE,UPDATE
            AS 
            BEGIN
            declare @event_type int=0;
            declare @productBrandId int;

            set @event_type=
            	(case when exists(select 1 from inserted) then 1 ---inserting
            			when exists(select 1 from deleted) then 3 ---deleting
            			else 2 end) ---updating

            if(@event_type=1)
            begin
            	--declare my_cursor cursor for select ProductBrandId from inserted;

            	--open my_cursor
            	--fetch next from my_cursor into @productBrandId

            	--while @@FETCH_STATUS=0
            	--begin fetch next from my_cursor

            		insert into [seplog].[ProductInventorieLogs]
            		([ProductBrandId], [Thickness], [WarehouseId], [ApproximateInventory], [FloorInventory], [MaxInventory], [MinInventory], 
            		[OrderPoint], [MainId], [CreatedBy], [Created], [IsActive], [LogTypeId])
            		select 
            		[ProductBrandId], [Thickness], [WarehouseId], [ApproximateInventory], [FloorInventory], [MaxInventory], [MinInventory], 
            		[OrderPoint], inserted.Id, [CreatedBy], [Created], [IsActive], 1
            		from inserted

            	--end
            	--close my_cursor;
            	--deallocate my_cursor;

            end

            SET NOCOUNT ON;
            END; disable trigger [TR_ProductInventoryEvents] on [sepdb].[ProductInventories];");


            //---------------Create Product Official Inventory----------------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_CreateProductOfficialInventory]    Script Date: 2/20/2024 10:44:18 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_CreateProductOfficialInventory]
               ON  [sepdb].[Products] 
               AFTER INSERT
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	declare 
            	 @newProductId nvarchar(1000),
            	 @warehouseId int,
            	 @warehouseTypeId int

            	DECLARE @CC1 VARCHAR(MAX)
            	DECLARE db_cursor1 CURSOR
            		FOR SELECT DISTINCT Id from inserted as w  

            	OPEN db_cursor1
            	FETCH NEXT FROM db_cursor1
            	INTO @newProductId

            	WHILE @@FETCH_STATUS = 0
            	Begin
            				DECLARE @CC VARCHAR(MAX)
            				DECLARE db_cursor CURSOR
            					FOR SELECT DISTINCT Id from sepdb.Warehouses as w where w.WarehouseTypeId=5
            						--and not exists (select Id from sepdb.OfficialWarehoseInventories 
            						--				  where ProductId=(select Id from inserted) and WarehouseId=Id)  

            				OPEN db_cursor
            				FETCH NEXT FROM db_cursor
            				INTO @warehouseId

            				WHILE @@FETCH_STATUS = 0

            				BEGIN
            					if(not exists(select * from [sepdb].[OfficialWarehoseInventories] 
            						where ProductId=@newProductId and WarehouseId=@warehouseId))
            						begin
            							insert into [sepdb].[OfficialWarehoseInventories] 
            								([ProductId], [Thickness], [WarehouseId], [ApproximateInventory], [FloorInventory], 
            								[MaxInventory], [MinInventory], [OrderPoint], [CreatedBy], [Created],[IsActive])
            								select
            								Id,0,@warehouseId,0,0,0,0,0,'882d46b7-9df8-49bd-a414-2074bcfbcf35',GETDATE(),1
            								from inserted where Id=@newProductId;
            						end

            					FETCH NEXT FROM db_cursor
            					INTO @warehouseId
            				END

            				CLOSE db_cursor
            				DEALLOCATE db_cursor

            	FETCH NEXT FROM db_cursor1
            		INTO @newProductId
            	END

            	CLOSE db_cursor1
            	DEALLOCATE db_cursor1

            END");

            //--------------------Prdouct Events------------------
            migrationBuilder.Sql(@"USE [SepehrDb]
            GO
            /****** Object:  Trigger [sepdb].[TR_ProductEvents]    Script Date: 2/20/2024 10:45:47 PM ******/
            SET ANSI_NULLS ON
            GO
            SET QUOTED_IDENTIFIER ON
            GO
            -- =============================================
            -- Author:		<Author,,Name>
            -- Create date: <Create Date,,>
            -- Description:	<Description,,>
            -- =============================================
            CREATE TRIGGER [sepdb].[TR_ProductEvents]
               ON  [sepdb].[Products]
               AFTER INSERT,DELETE,UPDATE
            AS 
            BEGIN
            	-- SET NOCOUNT ON added to prevent extra result sets from
            	-- interfering with SELECT statements.
            	SET NOCOUNT ON;

            	select * into #t from inserted
            	union (select * from deleted);

            	DECLARE @event_type int
            	IF EXISTS(SELECT * FROM inserted)
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 2
            	ELSE
            		SELECT @event_type = 1
            	ELSE
            	  IF EXISTS(SELECT * FROM deleted)
            		SELECT @event_type = 3
            	  ELSE
            		--no rows affected - cannot determine event
            		SELECT @event_type = 4

            	insert into [seplog].[ProductLogs]
            		([Id], [ProductCode], [Barcode], [ProductName], [ProductTypeId], [ProductSize],
            		[ProductThickness], [ApproximateWeight], [NumberInPackage], [ProductStandardId],
            		[ProductStateId], [ProductMainUnitId], [ProductSubUnitId], [ExchangeRate], 
            		[MaxInventory], [MinInventory], [InventotyCriticalPoint], [Description], [CreatedBy], [Created],
            		[LastModifiedBy], [LastModified], [IsActive], [LogTypeId], [MainId]	)
            	select top 1 
            		 (select NEWID()),
            		 [ProductCode], [Barcode], [ProductName], [ProductTypeId], [ProductSize],
            		 [ProductThickness], [ApproximateWeight], [NumberInPackage], [ProductStandardId],
            		 [ProductStateId], [ProductMainUnitId], [ProductSubUnitId], [ExchangeRate], 
            		 [MaxInventory], [MinInventory], [InventotyCriticalPoint], [Description], 
            		 [CreatedBy], [Created], [LastModifiedBy], [LastModified],
            		 [IsActive],@event_type,[Id]		 
            	from #t;

            	drop table #t;

            END; disable trigger TR_ProductEvents on [sepdb].[Products];");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop trigger [sepdb].[TR_OrderDetailLog]");
            migrationBuilder.Sql(@"drop trigger [sepdb].[TR_OrderPaymentLog]");
            migrationBuilder.Sql(@"drop trigger [TR_ProductEvents]");
            migrationBuilder.Sql(@"drop trigger [TR_CreateProductOfficialInventory]");
            migrationBuilder.Sql(@"drop trigger [TR_ProductInventoryEvents]");
            migrationBuilder.Sql(@"drop trigger [ProductInventoryEvents]");
            migrationBuilder.Sql(@"drop trigger [TR_ProductDetailEvents]");
            migrationBuilder.Sql(@"drop trigger [TR_ProductBrandEvents]");
            migrationBuilder.Sql(@"drop trigger [TR_CreateWarehouseInventory]");
            migrationBuilder.Sql(@"drop trigger [TR_OrderLog]");
            migrationBuilder.Sql(@"drop trigger [UpdateProductInventory]");
        }

    }
}
