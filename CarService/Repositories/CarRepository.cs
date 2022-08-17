using CarService.Dtos.RequestFeatures;
using CarService.Extentions.Utilities;
using CarService.Models;
using CarService.Repositories.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CarService.Repositories
{
    public class CarRepository : BaseRepository, ICarRepository
    {
        public CarRepository(IConfiguration configuration)
            : base(configuration)
        { }
        public async Task<List<Car>> GetAllAsync(CarParameters carParams)
        {
            List<Car> cars = new List<Car>();
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Car>(carParams.OrderBy.Trim(','));

            try
            {
                string procedure = "Car_GetAllWithOptions";

                using (SqlConnection connection = CreateConnection())
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = procedure,
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    cmd.Parameters.AddWithValue("@MinPrice", carParams.MinPrice);
                    cmd.Parameters.AddWithValue("@MaxPrice", carParams.MaxPrice);
                    cmd.Parameters.AddWithValue("@Company", carParams.Company);
                    cmd.Parameters.AddWithValue("@Model", carParams.Model);
                    cmd.Parameters.AddWithValue("@Color", carParams.Color);
                    cmd.Parameters.AddWithValue("@OrderBy", orderQuery);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Car car = new Car();
                        car.Id = Convert.ToInt32(reader[0]);
                        car.Company = reader[1].ToString();
                        car.Model = reader[2].ToString();
                        car.Color = reader[3].ToString();
                        car.Price = Convert.ToDouble(reader[4]);
                        cars.Add(car);
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return cars;
        }

        public async Task<string> GetCompaniesAsync()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                string procedure = "Company_GetAllDist";

                using (SqlConnection connection = CreateConnection())
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = procedure,
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sb.Append(reader.GetString(0)+',');
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return sb.ToString().Trim(',');
        }

        public async Task<string> GetModelsAsync()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                string procedure = "Model_GetAllDist";

                using (SqlConnection connection = CreateConnection())
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = procedure,
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sb.Append(reader.GetString(0) + ',');
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return sb.ToString().Trim(',');
        }

        public async Task<string> GetColorsAsync()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                string procedure = "Car_Colors_GetAllDist";

                using (SqlConnection connection = CreateConnection())
                {
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = procedure,
                        Connection = connection,
                        CommandType = CommandType.StoredProcedure
                    };
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        sb.Append(reader.GetString(0) + ',');
                    }
                    reader.Close();
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return sb.ToString().Trim(',');
        }
    }
}
