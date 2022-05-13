using System;
using System.Collections.Generic;

#nullable disable

namespace Abschlussprojekt_Fitnessstudio.DbModels
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Emailaddress { get; set; }
        public DateTime? Createdate { get; set; }
        public DateTime? RemovedDate { get; set; }
        public int? TrainingPlanId { get; set; }
        public int? Subscription { get; set; }

        public virtual Subscription SubscriptionNavigation { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
    }
}
