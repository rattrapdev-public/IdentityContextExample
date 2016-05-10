namespace Geonetric.Identity.Domain.Users
{
    using System;

    using Geonetric.DDD.Domain;

    public class Name : IValueObject, IEquatable<Name>
	{
		private string firstName;
		private string lastName;

		public Name (string firstName, string lastName)
		{
			this.FirstName = firstName;
			this.LastName = lastName;
		}

		public string FirstName 
		{
			get 
			{
				return this.firstName;
			}
			set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("First Name : Is invalid!");
				}

				this.firstName = value;
			}
		}

		public string LastName 
		{
			get 
			{
				return this.lastName;
			}
			set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("Last Name : Is invalid!");
				}

				this.lastName = value;
			}
		}

		public bool Equals (Name other)
		{
			if (other == null) 
			{
				return false;
			}

			if (ReferenceEquals (this, other)) 
			{
				return true;
			}

			return this.FirstName.Equals (other.FirstName) && this.LastName.Equals (other.LastName);
		}
	}
}

