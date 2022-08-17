using CarService.Extentions.Utilities;
using CarService.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using CarService.Dtos.RequestFeatures;

namespace CarService.Extentions
{
    public static class CarRepoExtensions
    {
		public static IQueryable<Car> FilterCars(this IQueryable<Car> cars, CarParameters carParams) =>
			from c in cars
				where c.Price >= carParams.MinPrice && (c.Price <= carParams.MaxPrice || carParams.MaxPrice == 0)
				where carParams.Company == null || carParams.Company == "" || carParams.Company.Contains(c.Company)
				where carParams.Model == null || carParams.Model == "" || carParams.Model.Contains(c.Model)
				where carParams.Color == null || carParams.Color == "" || carParams.Color.Contains(c.Color)
				select c;

		public static IQueryable<Car> Sort(this IQueryable<Car> cars, string orderByQueryString)
		{
			if (string.IsNullOrWhiteSpace(orderByQueryString))
				return cars.OrderBy(e => e.Company);

			var orderQuery = OrderQueryBuilder.CreateOrderQuery<Car>(orderByQueryString);

			if (string.IsNullOrWhiteSpace(orderQuery))
				return cars.OrderBy(e => e.Company);

			return cars.OrderBy(orderQuery);
		}

	}
}
