namespace Qualifier.Application.Database.Version.Queries.GetVersionById
{
    public class GetVersionByIdDto
    {
        public int versionId { get; set; }
        public decimal number { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public int confidentialityLevelId { get; set; }
        public int documentationId { get; set; }
        public DateTime date { get; set; }
        public bool isCurrent { get; set; }
        public string fileName { get; set; }
        public string description { get; set; }
        public int standardId { get; set; }
        public int companyId { get; set; }

    }
}

