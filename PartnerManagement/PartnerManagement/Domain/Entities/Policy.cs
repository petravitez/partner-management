namespace PartnerManagement.Domain.Entities
{
    public class Policy : Entity
    {
        public int PartnerId { get; private set; }
        public string PolicyNumber { get; private set; }
        public decimal PolicyAmount { get; private set; }

        public Policy(int partnerId, string policyNumber, decimal policyAmount)
        {
            ValidatePartnerId(partnerId);
            ValidatePolicyNumber(policyNumber);
            ValidatePolicyAmount(policyAmount);

            PartnerId = partnerId;
            PolicyNumber = policyNumber;
            PolicyAmount = policyAmount;
        }

        private void ValidatePartnerId(int partnerId)
        {
            if (partnerId <= 0)
                throw new ArgumentException("PartnerId must be a positive integer.");
        }

        private void ValidatePolicyNumber(string policyNumber)
        {
            if (string.IsNullOrWhiteSpace(policyNumber) || policyNumber.Length < 10 || policyNumber.Length > 15)
                throw new ArgumentException("Policy number must be between 10 and 15 characters.");
        }

        private void ValidatePolicyAmount(decimal policyAmount)
        {
            if (policyAmount <= 0)
                throw new ArgumentException("Policy amount must be greater than zero.");
        }

        public void SetPartnerId(int partnerId)
        {
            ValidatePartnerId(partnerId);
            PartnerId = partnerId;
        }

        public void SetPolicyNumber(string policyNumber)
        {
            ValidatePolicyNumber(policyNumber);
            PolicyNumber = policyNumber;
        }

        public void SetPolicyAmount(decimal policyAmount)
        {
            ValidatePolicyAmount(policyAmount);
            PolicyAmount = policyAmount;
        }
    }


}
