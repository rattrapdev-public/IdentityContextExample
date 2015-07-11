using System;

namespace RattrapDev.DDD
{
	public interface IValidationSpecification<TObject>
	{
		bool IsSatisifiedBy(TObject candidate);
	}
}

