using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.CargoAnnouncements.Queries.GetAllCargoAnncs
{
    public class GetAllCargoAnncsParameter : RequestParameter
    {
        public Guid? OrderId { get; set; }
        public long? OrderCode { get; set; }
        public Guid? CustomerId { get; set; }
        public bool IsCompletlyLading { get; set; }=false;
        public bool? HasExitPermit { get; set; }
    }
}