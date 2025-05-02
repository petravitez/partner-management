namespace PartnerManagement.Domain.Entities
{
    public class Partner : Entity
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string? Address { get; private set; }
        public string PartnerNumber { get; private set; }
        public string? CroatianPIN { get; private set; }
        public int PartnerTypeId { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public string CreatedByUser { get; private set; }
        public bool IsForeign { get; private set; }
        public string ExternalCode { get; private set; }
        public int GenderId { get; private set; }

        public Partner(
            string firstName,
            string lastName,
            string? address,
            string partnerNumber,
            string? croatianPIN,
            byte partnerTypeId,
            string createdByUser,
            bool isForeign,
            string externalCode,
            int genderId)
        {
            ValidateFirstName(firstName);
            ValidateLastName(lastName);
            ValidatePartnerNumber(partnerNumber);
            ValidateCroatianPIN(croatianPIN);
            ValidateCreatedByUser(createdByUser);
            ValidateExternalCode(externalCode);
            ValidateGenderId(genderId);

            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PartnerNumber = partnerNumber;
            CroatianPIN = croatianPIN;
            PartnerTypeId = partnerTypeId;
            CreatedAtUtc = DateTime.UtcNow; 
            CreatedByUser = createdByUser;
            IsForeign = isForeign;
            ExternalCode = externalCode;
            GenderId = genderId;
        }

        private void ValidateFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName) || firstName.Length < 2 || firstName.Length > 255)
                throw new ArgumentException("First name must be between 2 and 255 characters.");
        }

        private void ValidateLastName(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName) || lastName.Length < 2 || lastName.Length > 255)
                throw new ArgumentException("Last name must be between 2 and 255 characters.");
        }

        private void ValidatePartnerNumber(string partnerNumber)
        {
            if (partnerNumber.Length != 20 || !System.Text.RegularExpressions.Regex.IsMatch(partnerNumber, @"^\d{20}$"))
                throw new ArgumentException("Partner number must be exactly 20 digits.");
        }

        private void ValidateCroatianPIN(string? croatianPIN)
        {
            if (croatianPIN != null && !System.Text.RegularExpressions.Regex.IsMatch(croatianPIN, @"^\d{11}$"))
                throw new ArgumentException("Croatian PIN (OIB) must be exactly 11 digits.");
        }

        private void ValidateCreatedByUser(string createdByUser)
        {
            if (string.IsNullOrWhiteSpace(createdByUser) || createdByUser.Length > 255 || !createdByUser.Contains("@"))
                throw new ArgumentException("CreatedByUser must be a valid email address and cannot exceed 255 characters.");
        }

        private void ValidateExternalCode(string externalCode)
        {
            if (externalCode.Length < 10 || externalCode.Length > 20)
                throw new ArgumentException("External Code must be between 10 and 20 characters.");
        }

        private void ValidateGenderId(int genderId)
        {
            if (genderId <= 0)
                throw new ArgumentException("GenderId must be a valid reference.");
        }

        public void SetFirstName(string firstName)
        {
            ValidateFirstName(firstName);
            FirstName = firstName;
        }

        public void SetLastName(string lastName)
        {
            ValidateLastName(lastName);
            LastName = lastName;
        }

        public void SetAddress(string? address)
        {
            Address = address;
        }

        public void SetPartnerNumber(string partnerNumber)
        {
            ValidatePartnerNumber(partnerNumber);
            PartnerNumber = partnerNumber;
        }

        public void SetCroatianPIN(string? croatianPIN)
        {
            ValidateCroatianPIN(croatianPIN);
            CroatianPIN = croatianPIN;
        }

        public void SetPartnerTypeId(byte partnerTypeId)
        {
            PartnerTypeId = partnerTypeId;
        }

        public void SetCreatedByUser(string createdByUser)
        {
            ValidateCreatedByUser(createdByUser);
            CreatedByUser = createdByUser;
        }

        public void SetIsForeign(bool isForeign)
        {
            IsForeign = isForeign;
        }

        public void SetExternalCode(string externalCode)
        {
            ValidateExternalCode(externalCode);
            ExternalCode = externalCode;
        }

        public void SetGender(int genderId)
        {
            ValidateGenderId(genderId);
            GenderId = genderId;
        }
    }

}
