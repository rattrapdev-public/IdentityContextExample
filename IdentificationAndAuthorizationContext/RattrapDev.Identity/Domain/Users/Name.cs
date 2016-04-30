using System;
using RattrapDev.DDD;

namespace RattrapDev.Identity.Domain.Users
{
	public class Name : IValueObject, IEquatable<Name>
	{
		private string firstName;
		private string lastName;

		public Name (string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		public string FirstName 
		{
			get 
			{
				return firstName;
			}
			set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("First Name : Is invalid!");
				}

				firstName = value;
			}
		}

		public string LastName 
		{
			get 
			{
				return lastName;
			}
			set 
			{
				if (string.IsNullOrWhiteSpace (value)) 
				{
					throw new ArgumentException ("Last Name : Is invalid!");
				}

				lastName = value;
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

			return FirstName.Equals (other.FirstName) && LastName.Equals (other.LastName);
		}
	}
}

