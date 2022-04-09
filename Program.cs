using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace tpmodul7_1302204051
{
    class Program
    {
        public static void Main(string[] args)
        {
            ProgramConfig config = new ProgramConfig();

            Console.Write("Berapa suhu badana anda saat ini? ");
            string CONFIG1str = Console.ReadLine();
            double CONFIG1 = double.Parse(CONFIG1str);

            Console.Write("Berapa hari yang lalu (perkiraan) anda terakhir memiliki gejala demam? ");
            string CONFIG2str = Console.ReadLine();
            int CONFIG2 = int.Parse(CONFIG2str);
        }
    }

    class ProgramConfig
    {
        public CovidConfig conf;
        public string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public string configFileName = "covid_config.json";
        public ProgramConfig() 
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception ex)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }

        private void SetDefault()
        {
            conf = new CovidConfig("celcius", 14, "Anda tidak diperbolehkan masuk ke dalam gedung ini", "Anda dipersilahkan untuk masuk ke dalam gedung ini");

        }

        private CovidConfig ReadConfigFile()
        {
            string jsonFromFile = File.ReadAllText(path + '/' + configFileName);
            conf = JsonSerializer.Deserialize<CovidConfig>(jsonFromFile);
            return conf;
        }
        
        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            String jsonString = JsonSerializer.Serialize(conf, options);
            string fullPath = path + '/' + configFileName;
            File.WriteAllText(fullPath, jsonString);
        }
    }

    class CovidConfig
    {
        public string satuan_suhu { get; set; }
        public int batas_hari_demam { get; set; }
        public string pesan_ditolak { get; set; }
        public string pesan_diterima { get; set; }

        public CovidConfig() { }

        public CovidConfig(string satuan_suhu, int batas_hari_demam, string pesan_ditolak, string pesan_diterima)
        {
            this.satuan_suhu = satuan_suhu;
            this.batas_hari_demam = batas_hari_demam;
            this.pesan_ditolak = pesan_ditolak;
            this.pesan_diterima = pesan_diterima;
        }
    }
}
