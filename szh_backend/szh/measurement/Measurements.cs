using DbManager.Db;
using System;
using System.Collections.Generic;
using szh.cultivation;

namespace szh.measurement {
    public class Measurement : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public int measurement_type;
        public int avr_device;
        public double value;
        public DateTime date_time;
        #endregion

        public static Measurement GetMeasurement(int id) => GetMeasurements($"select * from measurement.measurements where id = {id}")[0];

        public static Measurement GetTemperatureInTunnel(int tunnelId) {

            foreach (var avrDevice in AvrDevice.GetAvrDevicesInTunnel(tunnelId)) {

                //ogolnie proponuje wszystkie temperatury dla wszystkich urzadzen - srednia
                //ale na razie tylko na podstawie jednego urzadzenia
                return GetMeasurements($"select * from measurement.measurements where avr_device = {avrDevice.id} and measurement_type = 1 order by date_time desc limit 1 ")[0];
            }
            return null;
        }

        public static List<Measurement> GetTemperatureInTunnel(int tunnelId, DateTime startDate, DateTime endDate) {
            foreach (var avrDevice in AvrDevice.GetAvrDevicesInTunnel(tunnelId)) {

                string query = $"select * from measurement.measurements where avr_device = {avrDevice.id} and measurement_type = 1 " +
                    $"and date_time >= '{startDate}' and date_time <= '{endDate}' order by date_time";

                return GetMeasurements(query);
            }
            return null;
        }

        #region AddMeasurement

        public static Measurement AddMeasurement(Measurement measurement) {
            return AddMeasurement(measurement.measurement_type, measurement.avr_device, measurement.value);
        }

        public static Measurement AddMeasurement(int measurement_type, int avr_device, double value) {
            Measurement lastMeasurement = new Measurement() { id = GetMax("measurement.measurements") };

            string sql = $"insert into measurement.measurements (id,measurement_type,avr_device,value,date_time) " +
                $"values ({lastMeasurement.id + 1},{measurement_type},{avr_device},{value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)},'{DateTime.Now}')";

            pgSqlSingleManager.ExecuteSQL(sql);
            var measurementResult = pgSqlSingleManager.ExecuteSQL($"select * from measurement.measurements where id = {lastMeasurement.id + 1}");

            Measurement newMeasurement = new Measurement {
                id = Int32.Parse(measurementResult[0]["id"]),
                value = Double.Parse(measurementResult[0]["value"]),
                avr_device = Int32.Parse(measurementResult[0]["avr_device"]),
                measurement_type = Int32.Parse(measurementResult[0]["measurement_type"]),
                date_time = DateTime.Parse(measurementResult[0]["date_time"])
            };

            AvrDevice.Update(newMeasurement.avr_device);

            return newMeasurement;
        }

        #endregion

        public static void DeleteMeasurements() {
            pgSqlSingleManager.ExecuteSQL($"delete from measurement.measurements");
        }

        private static List<Measurement> GetMeasurements(string query) {
            List<Measurement> measurements = new List<Measurement>();

            foreach (var measurement in pgSqlSingleManager.ExecuteSQL(query)) {
                Measurement newMeasurement = new Measurement() {
                    id = Int32.Parse(measurement["id"]),
                    value = Double.Parse(measurement["value"]),
                    avr_device = Int32.Parse(measurement["measurement_type"]),
                    measurement_type = Int32.Parse(measurement["measurement_type"]),
                    date_time = DateTime.Parse(measurement["date_time"])
                };

                measurements.Add(newMeasurement);
            }
            return measurements;
        }
    }
}
