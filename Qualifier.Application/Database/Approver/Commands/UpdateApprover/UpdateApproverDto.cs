using Qualifier.Common.Application.NotificationPattern;

namespace Qualifier.Application.Database.Approver.Commands.UpdateApprover
{
    public class UpdateApproverDto
    {
        public int approverId { get; set; }
        public int personalId { get; set; }
        public int responsibleId { get; set; }
        public int? updateUserId { get; set; }

        public void requiredFieldsValidation(Notification notification)
        {
        }

    }
}

