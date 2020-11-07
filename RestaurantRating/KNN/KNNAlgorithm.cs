using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestaurantRating.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace Blockbuster.KNN
    {
    class KNNAlgorithm
    {
        int K;
        int TotalTrainset;
        List<Restaurant> TrainSet;

        Distance[] distances;



        public KNNAlgorithm(int k, List<Restaurant> train)
        {
            this.K = k;

            this.TrainSet = train;
            this.TotalTrainset = train.Count;

            distances = new Distance[this.TotalTrainset];


        }

        public List<Restaurant> GetNearest(Restaurant restaurant)
        {


            //calculate all distances
            for (int i = 0; i < this.TotalTrainset; i++)
            {
                distances[i] = new Distance();
                distances[i].distance = 0;
                distances[i].index = i;
                /*

                double movieGenreId = (double)(Restaurant)Enum.Parse(typeof(Genre), movie.Genre.ToString());
                double testMovieGenreID = (double)(Genre)Enum.Parse(typeof(Genre), this.TrainSet[i].Genre.ToString());

                distances[i].distance = GetDistance(movieGenreId, testMovieGenreID, movie.PriceProduct, this.TrainSet[i].PriceProduct);
                */

            }

            //sort
            List<Distance> SortedDistances = distances.OrderBy(o => o.distance).ToList();

            List<Restaurant> NearestRestraunts = new List<Restaurant>();
            for (int i = 0; i < K; i++)
            {
                NearestRestraunts.Add(TrainSet[SortedDistances[i].index]);
            }

            return NearestRestraunts;

        }

        public double GetDistance(double a, double b, double c, double d)
        {
            return Math.Sqrt(((a - b) * (a - b) + (c - d) * (c - d))); ;
        }

    }
}