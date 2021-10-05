using System;
using Persistance;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace DAL
{
    public class CardDAL
    {
        private MySqlConnection connection = DbHelper.GetConnection();

        public List<Card> GetAllCard()
        {
            List<Card> cards = new List<Card>();
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = "select card_id, stat from cards where stat = 0";
                    MySqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Card card = new Card();
                        card.CardId = reader.GetInt32("card_id");
                        card.Stat = reader.GetInt32("stat");
                        cards.Add(card);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                finally
                {
                    connection.Close();
                }
            }

            return cards;
        }
        public void UpdateCards(int cardId, bool status)
        {   
            
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand command = connection.CreateCommand();
                    command.CommandText = @"UPDATE Cards
                                            SET stat = "+ status +" WHERE card_id = "+ cardId +"; ";
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                finally
                {
                    connection.Close();
                }
            }
        }
    }
}