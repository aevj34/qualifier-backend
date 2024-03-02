using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Indicator.Commands.UpdateIndicator
{
    public class UpdateIndicatorDto
    {
        public int indicatorId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string abbreviation { get; set; }
        public decimal value { get; set; }
        public string color { get; set; }
        public int companyId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {

            if (name == null)
                notification.addError("El name es obligatorio");

            if (description == null)
                notification.addError("El description es obligatorio");

            if (abbreviation == null)
                notification.addError("El abbreviation es obligatorio");

            if (value == null)
                notification.addError("El value es obligatorio");

            if (color == null)
                notification.addError("El color es obligatorio");

        }

    }
}

