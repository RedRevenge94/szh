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

            List<Measurement> measurements;

            foreach (var avrDevice in AvrDevice.GetAvrDevicesInTunnel(tunnelId)) {

                string query = $"select * from measurement.measurements where avr_device = {avrDevice.id} and measurement_type = 1 " +
                    $"and date_time >= '{startDate}' and date_time <= '{endDate}' order by date_time";

                measurements = GetMeasurements(query);

                if (measurements != null) {

                    List<Measurement> newMeasurementList = new List<Measurement>();

                    Measurement lastMeasurement = new Measurement();
                    bool isStart = true;

                    foreach (Measurement measurement in measurements) {

                        if (isStart) {
                            newMeasurementList.Add(measurement);
                            lastMeasurement = measurement;
                            isStart = false;
                        } else {
                            if (Math.Abs((measurement.date_time - lastMeasurement.date_time).TotalMinutes) > 10) {
                                newMeasurementList.Add(measurement);
                                lastMeasurement = measurement;
                            }
                        }

                    }

                    return newMeasurementList;
                }
            }

            return null;
        }

        #region AddMeasurement

        public static Measurement AddMeasurement(Measurement measurement) {
            return AddMeasurement(measurement.measurement_type, measurement.avr_device, measurement.value);
        }

        public static Measurement AddMeasurement(int measurement_type, int avr_device, double value) {

            DateTime date = DateTime.Now;

            try {
                string sql = $"insert into measurement.measurements (measurement_type,avr_device,value,date_time) " +
                $"values ({measurement_type},{avr_device},{value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)},'{date}')";

                pgSqlSingleManager.ExecuteSQL(sql);
                //to po dacie byc nie moze
                var measurementResult = pgSqlSingleManager.ExecuteSQL($"select * from measurement.measurements where" +
                    $" measurement_type = {measurement_type} and avr_device = {avr_device} and date_time = '{date}'");

                Measurement newMeasurement = new Measurement {
                    id = Int32.Parse(measurementResult[0]["id"]),
                    value = Double.Parse(measurementResult[0]["value"]),
                    avr_device = Int32.Parse(measurementResult[0]["avr_device"]),
                    measurement_type = Int32.Parse(measurementResult[0]["measurement_type"]),
                    date_time = DateTime.Parse(measurementResult[0]["date_time"])
                };

                AvrDevice.Update(newMeasurement.avr_device);

                return newMeasurement;
            } catch { }


            return null;
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
