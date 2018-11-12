using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.cultivation {
    public class CultivationComment : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public string text;
        public int cultivation;
        public DateTime? create_date;
        #endregion

        public static CultivationComment GetCultivationComment(int id) {
            return GetCultivationComments($"select * from cultivation.cultivation_comments where id = {id}")[0];
        }

        public static List<CultivationComment> GetCultivationComments(int breedingId) {
            return GetCultivationComments($"select * from cultivation.cultivation_comments where cultivation = {breedingId}");
        }

        public static CultivationComment AddCultivationComents(string text, int breeding) {

            Cultivation lastBreedingComments = new Cultivation() { id = GetMax("cultivation.cultivation_comments") };

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.cultivation_comments (id,text,cultivation,create_date) " +
                $"values ({lastBreedingComments.id + 1},'{text}',{breeding},'{DateTime.Now}')");
            var breedingCommentResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.cultivation_comments where id = {lastBreedingComments.id + 1}");

            CultivationComment newBreedingComment = new CultivationComment {
                id = Int32.Parse(breedingCommentResult[0]["id"]),
                text = breedingCommentResult[0]["text"],
                cultivation = Int32.Parse(breedingCommentResult[0]["cultivation"]),
                create_date = DateTime.Parse(breedingCommentResult[0]["create_date"])
            };

            return newBreedingComment;
        }

        public static void DeleteFromBreeding(int breedingId) {
            pgSqlSingleManager.ExecuteSQL($"delete from cultivation.cultivation_comments where cultivation = {breedingId}");
        }

        private static List<CultivationComment> GetCultivationComments(string query) {
            List<CultivationComment> breedingComments = new List<CultivationComment>();

            foreach (var breedingComment in pgSqlSingleManager.ExecuteSQL(query)) {
                CultivationComment newBreedingComment = new CultivationComment() {
                    id = Int32.Parse(breedingComment["id"]),
                    text = breedingComment["text"],
                    cultivation = Int32.Parse(breedingComment["cultivation"]),
                    create_date = null
                };
                if (breedingComment["create_date"] != "") {
                    newBreedingComment.create_date = DateTime.Parse(breedingComment["create_date"]);
                }
                breedingComments.Add(newBreedingComment);
            }
            return breedingComments;
        }
    }

}

