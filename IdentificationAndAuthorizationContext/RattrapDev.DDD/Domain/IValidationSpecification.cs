namespace Geonetric.DDD.Domain
{
    public interface IValidationSpecification<TObject>
	{
		bool IsSatisifiedBy(TObject candidate);
	}
}

