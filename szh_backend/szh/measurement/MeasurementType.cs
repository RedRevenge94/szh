using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.measurement {
    public class MeasurementType : Entity {

        public int id;
        public string name;
        public string acronym;

        public static string GetNameOfMeasurementType(int id) {
            return pgSqlSingleManager.ExecuteSQL($"select name from measurement.measurement_type where id = {id}")[0]["name"];
        }

        public static List<MeasurementType> GetMeasurementTypes() {
            return GetMeasurementType($"select * from measurement.measurement_type");
        }

        private static List<MeasurementType> GetMeasurementType(string query) {
            List<MeasurementType> measurementTypes = new List<MeasurementType>();

            foreach (var measurementType in pgSqlSingleManager.ExecuteSQL(query)) {
                MeasurementType newMeasurementType = new MeasurementType() {
                    id = Int32.Parse(measurementType["id"]),
                    name = measurementType["name"],
                    acronym = measurementType["acronym"]
                };
                measurementTypes.Add(newMeasurementType);
            }
            return measurementTypes;
        }

    }
}
