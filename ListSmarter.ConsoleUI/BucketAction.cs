using System.Text.Json;
using ListSmarter.Controllers;
using ListSmarter.Models;

namespace ListSmarter.ConsoleUI
{
    public class BucketAction
	{
		private BucketController _bucketController;
		public BucketAction(BucketController bucketController)
		{
			_bucketController = bucketController;
		}

        public void getAll()
        {
            try
            {
                Console.WriteLine("Action -> Retrieve bucket list");
                var result = _bucketController.GetBuckets();
                if (result != null)
                {
                    result.ForEach(bucket => {
                        Console.WriteLine(JsonSerializer.Serialize(bucket));
                    });
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void getOne()
        {
            try
            {
                Console.WriteLine("Action -> Retrieve a specific bucket");
                Console.Write("Enter Bucket ID: ");
                var bucketId = Console.ReadLine();
                var result = _bucketController.GetBucket(bucketId);
                if (result != null)
                {
                    Console.WriteLine("Bucket information");
                    Console.WriteLine(JsonSerializer.Serialize<BucketDto>(result));
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

		public void create()
		{
            try
            {
                Console.WriteLine("Action -> Creating a bucket");
                Console.Write("Enter Bucket Name: ");
                string bucketName = Console.ReadLine();
                BucketDto bucketData = new BucketDto() { Title = bucketName };
                var result = _bucketController.CreateBucket(bucketData);
                if (result != null)
                {
                    Console.WriteLine(JsonSerializer.Serialize<BucketDto>(result));
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue(); 

            }
        }

        public void update()
        {
            try
            {
                Console.WriteLine("Action -> Updating a bucket");
                Console.Write("Enter Bucket ID: ");
                string id = Console.ReadLine();

                Console.Write("Enter Bucket Name: ");
                string bucketTitle = Console.ReadLine();

                BucketDto bucketData = new BucketDto() { Title = bucketTitle };
                var result = _bucketController.UpdateBucket(id, bucketData);
                if (result != null)
                {
                    Console.WriteLine($"Bucket with ID {id} was updated successfully.\n");
                    Console.WriteLine(JsonSerializer.Serialize<BucketDto>(result));
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void delete()
        {
            try
            {
                Console.WriteLine("Action -> Delete a bucket");
                Console.WriteLine("Enter Bucket ID: ");
                string bucketId = Console.ReadLine();

                var result = _bucketController.DeleteBucket(bucketId);
                if (result != null)
                {
                    Console.WriteLine($"Bucket with ID {bucketId} was deleted successfully.\n");
                    Console.WriteLine(JsonSerializer.Serialize<BucketDto>(result));
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }
            finally
            {
                PressAnyKeyToContinue();
            }
        }

        public void LogError(string message)
        {
            Console.Error.WriteLine(message);
        }

        public void PressAnyKeyToContinue()
        {
            Console.Write("\nPress any key to continue...\n");
            Console.ReadKey();
        }
	}
}

