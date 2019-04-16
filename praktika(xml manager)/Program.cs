using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace XmlManager
{

    public class Product
    {
        public string Seller; // valyes
        public string Name;
        public int Quantity = 0;
        public int Price = 0;
        public string Date_of_sale = "0";

        public Product(string sl, string nm, int qn, int pr, string dfs)
        {

            Seller = sl;
            Name = nm;
            Quantity = qn;
            Price = pr;
            Date_of_sale = dfs;
        }
        public string Header = String.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10} | {4,-10}", "Seller", "Name", "Quantity", "Price", "Date_Of_Sale"); //  Header for starting table
        public string Table()
        {
            return String.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10} | {4,-10}", Seller, Name, Quantity, Price, Date_of_sale); // outputs a nice table of values
        }
        public const string value = "2. Seller 3. Name 4. Quantity 5. Price 6. Date_Of_Sale";

        public Dynam ToDynam()
        {
            return new Dynam(Seller, Name, Quantity, Price, Date_of_sale); // converts Product structure to Dynamic structure when called
        }
        public void ToXml(XmlWriter wr) // writes product sstructure xml file
        {

            wr.WriteStartElement("Product");

            wr.WriteElementString("Seller", Seller);
            wr.WriteElementString("Name", Name);
            wr.WriteElementString("Quantity", Quantity.ToString());
            wr.WriteElementString("Price", Price.ToString());
            wr.WriteElementString("Date_of_sale", Date_of_sale.ToString());

            wr.WriteEndElement();
        }
        public Product ToCopy() { return new Product(Seller, Name, Quantity, Price, Date_of_sale); }
        public static Product FromXml(XmlElement el) // Read from xml and returns new structure with corresponding values from the XmlElement given
        {
            string[] names = { "Seller", "Name", "Quantity", "Price", "Date_of_sale" };
            string[] values = new string[names.Length];

            if (el.Name != "Product") return null;

            for (int i = 0; i < names.Length; i++)
            {
                XmlNodeList elst = el.GetElementsByTagName(names[i]);
                if (elst.Count != 0) values[i] = elst[0].InnerText;
                else return null;
            }
            int quant, price;
            string date = values[4];

            if (!(int.TryParse(values[2], out quant) && int.TryParse(values[3], out price))) return null;

            return new Product(values[0], values[1], quant, price, date);

        }
    }
    public class Train // Another structure
    {
        public int ID = 1;
        public string arr_date = "";
        public string start_date = "";
        public string direction = "";
        public int Length = 1;

        public Train(int i, string arr, string start, string dir, int len)

        {
            ID = i;
            arr_date = arr;
            start_date = start;
            direction = dir;
            Length = len;

        }
        public Dynam ToDynam() // converts Train to Dynam structure
        {
            return new Dynam(ID, arr_date, start_date, direction, Length);
        }

        public string Table() => string.Format("{0,-5} | {1,-15} | {2,-15} | {3,-10} | {4,-10}", ID.ToString(), arr_date, start_date, direction, Length.ToString()); // For outputs nice table
        public string Header() => string.Format("{0,-5} | {1,-15} | {2,-15} | {3,-10} | {4,-10}", "ID", "arrival_date", "Start_date", "direction", "Length");
        public const string value = "2. ID 3. arrival_date 4. Start_date 5. direction 6. Length";
        public void ToXml(XmlWriter wr) // writes structure to xml
        {
            wr.WriteStartElement("Train");

            wr.WriteElementString("ID", ID.ToString());
            wr.WriteElementString("arr_date", arr_date);
            wr.WriteElementString("start_date", start_date);
            wr.WriteElementString("direction", direction);
            wr.WriteElementString("Length", Length.ToString());

            wr.WriteEndElement();
        }
        public static Train FromXml(XmlElement el) // reads from xml and returns new Train structure with values from XmlElement. None if sth is wrong
        {
            string[] names = { "ID", "arr_date", "start_date", "direction", "Length" };
            string[] values = new string[names.Length];

            if (el.Name != "Train") return null;

            for (int i = 0; i < names.Length; i++)
            {
                XmlNodeList elst = el.GetElementsByTagName(names[i]);
                if (elst.Count != 0) values[i] = elst[0].InnerText;
                else return null;
            }

            int id = 0;
            int.TryParse(values[0], out id);
            int tem = 0;
            int.TryParse(values[4], out tem);
            return new Train(id, values[1], values[2], values[3], tem);


        }
        public static void Redaktor(string path, string node)
        {
            XmlDocument Doc = new XmlDocument();
            Doc.Load(path);


            XmlNodeList nodes = Doc.SelectNodes("/Product/" + node);
            foreach (XmlNode aNode in nodes)
            {
                XmlAttribute idAttribute = aNode.Attributes[node];
                idAttribute.InnerText = Console.ReadLine();
            }



            Doc.Save(path);

        }


    }
    public class Cars // Cars structure
    {
        public string CarBrand;
        public string Proizvoditel;
        public string CarType;
        public int Year;
        public string DateOfRegistration;



        public Cars(string C, string Pr, string CT, int YY, string DOR)
        {
            CarBrand = C;
            Proizvoditel = Pr;
            CarType = CT;
            Year = YY;
            DateOfRegistration = DOR;
        }
        public string Header = String.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10} | {4,-10}", "CarBrand", "Proizvoditel", "Car_Type", 2007, "Date_Of_Registration"); // returns Header for table start
        public string Table() => string.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10} | {4,-10}", CarBrand, Proizvoditel, CarType, Year, DateOfRegistration);// returns nice table to print all the values
        public const string value = "2. CarBrand 3. Proizvoditel 4. Car_type 5. Year 6. Date_of_Registration";

        public void ToXml(XmlWriter wr) //  writes values to xml file
        {

            wr.WriteStartElement("Cars");

            wr.WriteElementString("CarBrand", CarBrand);
            wr.WriteElementString("Proizvoditel", Proizvoditel);
            wr.WriteElementString("CarType", CarType);
            wr.WriteElementString("Year", Year.ToString());
            wr.WriteElementString("DateOfRegistration", DateOfRegistration);

            wr.WriteEndElement();
        }
        public Dynam ToDynam() // converts Cars to Dynam structure when called
        {
            return new Dynam(CarBrand, Proizvoditel, CarType, Year, DateOfRegistration);
        }
        public static Cars FromXml(XmlElement el) // Reads values from xml file and forms a structure from them. If some values are of wrong type returns null
        {
            string[] names = { "CarBrand", "Proizvoditel", "CarType", "Year", "DateOfRegistration" };
            string[] values = new string[names.Length];

            if (el.Name != "Cars") return null;

            for (int i = 0; i < names.Length; i++)
            {
                XmlNodeList elst = el.GetElementsByTagName(names[i]);
                if (elst.Count != 0) values[i] = elst[0].InnerText;
                else return null;
            }
            int year;
            string type = values[1];
            string date = values[4];

            if (!int.TryParse(values[3], out year)) return null;

            return new Cars(values[0], values[1], type, year, date);

        }
    }
    public class Information // Another structure
    {
        public int ID { get; set; } = -1; // someone forgot about that one
        public string Surname;
        public string Name;
        public string Position;
        public string Gender;
        public string On_work_date;
        public Information(string sn, string nm, string pst, string gnr, string owd)
        {

            Surname = sn;
            Name = nm;
            Position = pst;
            Gender = gnr;
            On_work_date = owd;

        }
        public string Header = String.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10} | {4, -10}", "Surname", "Name", "Position", "Gender", "On_work_date");
        public string Table() => string.Format("{0,-20} | {1,-10} | {2,-10} | {3,-10} | {4, -10}", Surname, Name, Position, Gender, On_work_date);
        public const string value = "2. Surname 3. Name 4. Position 5. Gender 6. On_work_date";

        public void ToXml(XmlWriter wr)
        {

            wr.WriteStartElement("Information");

            wr.WriteElementString("Surname", Surname);
            wr.WriteElementString("Name", Name);
            wr.WriteElementString("Position", Position);
            wr.WriteElementString("Gender", Gender);
            wr.WriteElementString("On_work_date", On_work_date);

            wr.WriteEndElement();
        }
        public Dynam ToDynam()
        {
            return new Dynam(Surname, Name, Position, Gender, On_work_date);
        }
        public static Information FromXml(XmlElement el)
        {
            string[] names = { "Surname", "Name", "Position", "Gender", "On_work_date" };
            string[] values = new string[names.Length];

            if (el.Name != "Information") return null;

            for (int i = 0; i < names.Length; i++)
            {
                XmlNodeList elst = el.GetElementsByTagName(names[i]);
                if (elst.Count != 0) values[i] = elst[0].InnerText;
                else return null;
            }



            return new Information(values[0], values[1], values[2], values[3], values[4]);

        }
    }
    public class Dynam // This class is a middleman between all other classes. If program calls any function inside the structure, the function will be rerouted to another structure and then returned back to the caller.
    {
        public dynamic one;
        public dynamic two;
        public dynamic three;
        public dynamic four;
        public dynamic five;
        public Dynam(dynamic o, dynamic tw, dynamic th, dynamic f, dynamic fi)
        {
            one = o;
            two = tw;
            three = th;
            four = f;
            five = fi;
        }
        public string Table() //  When it is called, it determines what structure is currently in use and then Table() from  one of the structures and returns it.
        {

            if (Program.curState == 0)
            {
                return new Product(one, two, three, four, five).Table(); // For Example: Here it calls Product with its own values.
            }
            else if (Program.curState == 1)
            {
                return new Train(one, two, three, four, five).Table();

            }
            else if (Program.curState == 2)
            {
                return new Cars(one, two, three, four, five).Table();
            }
            else if (Program.curState == 3)
            {
                return new Information(one, two, three, four, five).Table();
            }
            else
            {
                return null;
            }
        }
        public string Header() //  This one is supposed to route the caller to another structure
        {

            if (Program.curState == 0)
            {
                return new Product(null, null, 0, 0, null).Header;
            }
            else if (Program.curState == 1)
            {
                return new Train(1, null, null, null, 0).Header();
            }
            else if (Program.curState == 2)
            {
                return new Cars(null, null, null, 1, null).Header;
            }
            else if (Program.curState == 3)
            {
                return new Information(null, null, null, null, null).Header;
            }
            else
            {
                return null;
            }

        }
        public string Value() // Reroute again
        {
            string t = "";
            switch (Program.curState)
            {

                case 0:
                    t = Product.value;
                    break;
                case 1:
                    t = Train.value;
                    break;
                case 2:
                    t = Cars.value;
                    break;
                case 3:
                    t = Information.value;
                    break;
            }
            return t;
        }
        public void ToXml(XmlWriter wr) //  reroutes to ToXml function in other structures based on current state
        {
            switch (Program.curState)
            {
                case 0:
                    Product temp = new Product(one, two, three, four, five);
                    temp.ToXml(wr);
                    break;
                case 1:
                    Train tempTrain = new Train(one, two, three, four, five);
                    tempTrain.ToXml(wr);
                    break;
                case 2:
                    Cars car = new Cars(one, two, three, four, five);
                    car.ToXml(wr);
                    break;
                case 3:
                    Information info = new Information(one, two, three, four, five);
                    info.ToXml(wr);
                    break;

            }

        }
        public static Dynam FromXml(XmlElement el) // gets back the required structure > converts it to Dynam and returns it to the caller.
        {
            if (Program.curState == 0)
            {
                Product temp = Product.FromXml(el); //  Gets Product structure
                return temp.ToDynam(); //  Converts to Dynam and returns it
            }
            else if (Program.curState == 1)
            {
                Train tr = Train.FromXml(el);
                return tr.ToDynam();
            }
            else if (Program.curState == 2)
            {
                Cars car = Cars.FromXml(el);
                return car.ToDynam();
            }
            else if (Program.curState == 3)
            {
                Information info = Information.FromXml(el);
                return info.ToDynam();
            }
            else
            {
                return null;
            }

        }
        public Dynam ToCopy() { return new Dynam(one, two, three, four, five); }


    }

    class Program
    {

        public static int curState = 0;
        static void Main(string[] args)
        {
            ObjectStore app = new ObjectStore();

            app.ExitSave();
            app.Run();
        }
    }


    public class ObjectStore // Application Class
    {
        public int count = 0;
        public enum AppState
        {
            MainMenu,
            Config, // Currently only lets to edit file destination
            Export, // Export structure/structures to xml file based on users values inputed in Console
            ListData, // Lists the base data, can sort data by columns, name sorting is not entirely working
            Insert,
            Remove,
            ClearAll,
            DataModify,
            ViewOther, // View other files in other destinations inputed by user
            Modify,
            ExitSave,
            Exit
            //Plus any other state of operation
        }
        public static AppState state = AppState.MainMenu; // Default state of program
        public static List<Dynam> data = new List<Dynam>();
        public static string baseConfigPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\objstore\"; // it gets changed when user uses set destination void (898)
        public static string baseDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\objstore\"; // baseDataPath is set to %APPDATA% at first, then it is changed according to user request (base + Train\Product\Info\Cars +.xml)
        public static string[] fileNames = new string[] { "baseProduct.xml", "baseTrain.xml", "baseCar.xml", "baseInfo.xml" }; //  these are fileNames for different structures that are added later to baseDataPath
        public static string[] headNames = new string[] { "Product", "Train", "Cars", "Information" }; //  these are used when writing xml files (ViewOther, Save function)
        public static string dataPathFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\objstore\"; // this is default folder which shows where base files and journal logs are written
        public void ExitSave() //  ExitSave() is called when user is finished with operations menu and returns to Structure Menu. At first it will Save the base to a corresponding file and return exception if failed. Then it asks user for structure or lets him exit the program. Its another task is to check if user changed configuration folder and in that case stops the log at the previous destination and start a new one in the new destination as well as saves the database to the new location if it exists(465-488).
        {
            try
            {
                if (count == 0)
                {
                    File.WriteAllText(dataPathFolder + "journal.log", $"Starting objstore. Current Date:[{DateTime.Now.ToString("dd/MM/yyyy")}] " + Environment.NewLine);

                }
                if (baseConfigPath != dataPathFolder)
                {
                    File.AppendAllText(dataPathFolder + "journal.log", $"Stopping objstore. Current Date:[{DateTime.Now.ToString("dd/MM/yyyy")}] (Journal will be restarted in another location as configuration was changed during runtime)" + Environment.NewLine);

                    dataPathFolder = baseConfigPath;
                    baseDataPath = dataPathFolder + fileNames[Program.curState];
                    File.WriteAllText(dataPathFolder + "journal.log", $"Starting objstore. Current Date:[{DateTime.Now.ToString("dd/MM/yyyy")}] " + Environment.NewLine);

                }
                Save();
                Console.WriteLine("\nCurrent structures database saved");

            }
            catch (Exception e)
            {
                Console.WriteLine("Could not save changes to database: " + e.Message);
            }

            Console.WriteLine("\nSet current structure (0 - Product, 1 - Train, 2 - Cars, 3 - Information, 4 - Exit )"); // Here it asks user for the curState or lets him exit the program
            int.TryParse(Console.ReadLine(), out Program.curState);
            if (Program.curState == 4) // 
            {
                File.AppendAllText(dataPathFolder + @"\journal.log", $"Stopping objstore. Current Date:[{DateTime.Now.ToString("dd/MM/yyyy")}] " + Environment.NewLine); // stops the journal
                state = AppState.Exit;
                return;
            }
            Start();
            state = AppState.MainMenu;
        }
        public void Run()
        {

            // Keep running till state = exit, here all state functions are defined
            while (state != AppState.Exit)
            {
                switch (state)
                {
                    case AppState.ClearAll:
                        ClearAllData();
                        break;
                    case AppState.MainMenu:
                        MainMenu();
                        break;
                    case AppState.Export:
                        Export();
                        break;
                    case AppState.ListData:
                        ListNSort();
                        break;
                    case AppState.Insert:
                        AddingLine();
                        break;
                    case AppState.Remove:
                        Deletor();
                        break;
                    case AppState.DataModify:
                        DataModify();
                        break;
                    case AppState.ExitSave:
                        ExitSave();
                        break;
                    case AppState.Config:
                        SetDestination();
                        break;
                    case AppState.ViewOther:
                        ViewOther();
                        break;
                    case AppState.Exit:
                        ;
                        break;
                    default:
                        state = AppState.MainMenu;
                        break;
                }
            }
        }

        // Other functions that will be used with states:


        public void ClearAllData() //  Clears all database with users permission
        {
            Console.Write("Are you sure you want to empty the database ? y/n");
            if (Console.ReadKey().KeyChar == 'y')
            {
                data.RemoveRange(0, data.Count);
                Journal("Cleared database");
                state = AppState.MainMenu;
            }
        }
        int operationsCount = 0;
        public void MainMenu() // Changes states based on the input
        {
            Console.WriteLine("\nChoose an operation: ");
            Console.WriteLine("1. List Data");
            Console.WriteLine("2. Export");
            Console.WriteLine("3. Change configuration");
            Console.WriteLine("4. Remove recording from the base");
            Console.WriteLine("5. View other file.xml");
            Console.WriteLine("6. Insert recording into base");
            Console.WriteLine("7. Modify recording in the base");
            Console.WriteLine("8. Empty database");
            Console.WriteLine("9. Exit");
            operationsCount++;
            char key = '\0';
            do
            {
                Console.Write("Choice: ");
                key = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");

                switch (key)
                {
                    case '1': state = AppState.ListData; break;
                    case '2': state = AppState.Export; break;
                    case '3': state = AppState.Config; break;
                    case '4': state = AppState.Remove; break;

                    case '5': state = AppState.ViewOther; break;
                    case '6': state = AppState.Insert; break;
                    case '7': state = AppState.DataModify; break;
                    case '8': state = AppState.ClearAll; break;
                    case '9': state = AppState.ExitSave; break;
                    default: Console.WriteLine($"Unknown operation '{key}'."); break;
                }
            } while (key < '1' && key > '9');
        }
        public void loadDefaultValues() //  loads default values when called according to current state of Program
        {
            if (Program.curState == 0)
            {
                data.Add(new Product("Gum", "Vitek", 16, 9, "  21 Sep").ToDynam());
                data.Add(new Product("Cheese", "Kaja", 6, 6, "  21 Sep").ToDynam());
                data.Add(new Product("Rice", "Petja", 9, 16, "  21 Sep").ToDynam());
                data.Add(new Product("Apple", "Asja", 9, 10, "  21 Sep").ToDynam());
            }
            else if (Program.curState == 1)
            {
                data.Add(new Train(2, "25 September", "20 September", "West", 250).ToDynam());
                data.Add(new Train(3, "19 January", "13 February", "North", 250).ToDynam());
                data.Add(new Train(4, "21 November", "25 November", "East", 250).ToDynam());
                data.Add(new Train(5, "11 January", "6 January", " praktika", 250).ToDynam());
            }
            else if (Program.curState == 2)
            {
                data.Add(new Cars("defaultCar", "Volvo", "Mercedes", 1987, "2001-09-12").ToDynam());
                data.Add(new Cars("Ipay", "Apple", "Icar", 2020, "2020-09-12").ToDynam());
                data.Add(new Cars("OSCar", "Lucasfilm", "Fighter", 1987, "2001-09-12").ToDynam());
                data.Add(new Cars("No Idea", "Volvo", "WhoKnows", 1987, "2002-09-12").ToDynam());
            }
            else if (Program.curState == 3)
            {
                data.Add(new Information("Goldstein", "Mishel", "Banking", "Woman", "2001-12-12").ToDynam());
                data.Add(new Information("Goldstein", "Avraam", "Banking", "Man", "2002-12-12").ToDynam());
                data.Add(new Information("Rubinstein", "Vij", "Car repair", "Man", "2004-12-12").ToDynam());
                data.Add(new Information("Default", "Defualt", "Default", "Default", "2001-1-12").ToDynam());
            }
            Journal($"Loaded default values for {headNames[Program.curState]} structure");
        }
        public void Start()
        {
            data.Clear(); // Clears the previous database
            loadDefaultValues(); // loads new default values
            baseDataPath = dataPathFolder + fileNames[Program.curState]; // sets new baseDataPath (it is renewed each time structure is changed)

            if (!Directory.Exists(dataPathFolder)) // checks if Folder actually exists and creates if it doesnt
            {
                Directory.CreateDirectory(dataPathFolder);
            }
            if (!File.Exists(baseDataPath)) // checks whether or not File exists and creates one if required
            {
                Save();
            }
            else // File does exist at that point
            {
                try //  tries to load the .xml dataBase
                {

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(baseDataPath);
                    data.Clear();
                    foreach (XmlElement el in xDoc.DocumentElement.ChildNodes)
                    {
                        Dynam i = Dynam.FromXml(el);
                        if (i != null)
                        {
                            data.Add(i);
                        }
                    }
                    Journal($"Loaded {fileNames[Program.curState]} file to database");
                }
                catch (Exception) //  in case there are any errors in the .xml file the dataBase is loaded with new values and .xml file is re-written with the new dataBase
                {
                    Console.Write("Something is wrong with the base file, loading new base.xml with default values");
                    loadDefaultValues();
                    Save();
                    Journal($"Base file was missing or corrupt, database loaded with default values and saved to new {fileNames[Program.curState]} file");

                }

            }

        }
        void AddingLine() // Inserts line
        {
            // initialzing string values
            string prn = ""; // one
            string slrn = ""; //two
            string qnt = ""; //three
            string prc = ""; //four
            string dof = ""; //five
                             //part where user needs to type his values

            Console.WriteLine("Please type field1");
            prn = Console.ReadLine();

            Console.WriteLine("Please type field2");
            slrn = Console.ReadLine();

            Console.WriteLine("Please type field3");
            qnt = Console.ReadLine();

            Console.WriteLine("Please type field4");
            prc = Console.ReadLine();

            Console.WriteLine("Please type field5");
            dof = Console.ReadLine();



            try // this part makes sure that all values were of valid according to the current structure type and adds them to the dataBase
            {
                if (Program.curState == 0)
                {
                    int a = 0, b = 0;
                    bool i = int.TryParse(qnt, out a);
                    bool k = int.TryParse(prc, out b);
                    DateTime odate;
                    bool f = DateTime.TryParse(dof, out odate); // Checks datetime
                    if (!i || !k || !f)
                    {
                        Console.WriteLine($"Inputted lines {qnt} or {prc} were of wrong type and were set to 0 as default");
                    }
                    data.Add(new Product(prn, slrn, a, b, odate.ToString("yyyy-MM-dd")).ToDynam());
                }
                else if (Program.curState == 1)
                {
                    int a = 0, b = 0;
                    bool i = int.TryParse(prn, out a);
                    bool k = int.TryParse(dof, out b);
                    DateTime start;
                    DateTime finish;
                    bool arr = DateTime.TryParse(slrn, out finish);
                    bool st = DateTime.TryParse(qnt, out start);
                    if (!i || !k || !arr || !st)
                    {
                        Console.WriteLine($"Inputted lines {prn}, {dof}, {slrn} or {qnt} were of wrong type and were set to 0 or default value");
                    }
                    data.Add(new Train(a, finish.ToString("yyyy-MM-dd"), start.ToString("yyyy-MM-dd"), prc, b).ToDynam());
                }
                else if (Program.curState == 2)
                {
                    int a = 0;
                    bool i = int.TryParse(prc, out a);
                    DateTime prodTime;
                    bool st = DateTime.TryParse(dof, out prodTime);
                    if (!i || !st)
                    {
                        Console.WriteLine($"Inputted line/lines {dof} or {prc} were of wrong type and were set to 0 or default value");
                    }
                    data.Add(new Cars(prn, slrn, qnt, a, prodTime.ToString("yyyy-MM-dd")).ToDynam());
                }
                else if (Program.curState == 3)
                {
                    DateTime tempor;
                    bool st = DateTime.TryParse(dof, out tempor);
                    if (!st)
                    {
                        Console.WriteLine($"Inputted line {dof} was of wrong type and was set to 0 or default value");
                    }
                    data.Add(new Information(prn, slrn, qnt, prc, tempor.ToString("yyyy-MM-DD")).ToDynam());
                }
            }
            catch (Exception a)// if user types wrong information (for example instead of int he types string) "catch part will notify user that mistake was made"
            {
                Console.WriteLine("Something caused a problem! Here's the error message: {0}" + a.Message);
            }
            Journal($"Added line to database");

            state = AppState.MainMenu;


        }
        public void Deletor()
        {
            Console.WriteLine("\n Showing all available recordings: ");
            for (int i = 0; i < data.Count; i++) // Lists all available records with indexes 
            {
                Console.WriteLine($"{i}: {data[i].Table()}");
            }
            Console.WriteLine("Are you sure you want to proceed ? This operation might cause potential loss of data. y/n");
            if (Console.ReadKey().KeyChar == 'y')
            {
                Console.WriteLine("\n Enter index ");
                int index = 0;
                if (!int.TryParse(Console.ReadLine(), out index))// gets index
                {
                    do
                    {
                        Console.Write("\n invalid value:");
                    } while (!int.TryParse(Console.ReadLine(), out index));
                }
                Console.Write("\n Are you sure you want to delete this recording ? y/n");
                if (Console.ReadKey().KeyChar == 'y')
                {
                    data.Remove(data[index]);
                }
                else
                {
                    Deletor(); // restarts the void
                }
                Journal($"Deleted recording at index {index} from the database");
            }
            else // returns to main menu
            {
                state = AppState.MainMenu;
                MainMenu();
            }
        }
        public void ViewOther()
        {

            XmlDocument xDoc = new XmlDocument();
            Console.WriteLine("Enter destination:");
            string xmlfile = Console.ReadLine();
            List<Dynam> products = new List<Dynam>();

            try // gets the xml file and tries to read it. If xmls file structure is invalid catch(Exception) is called and void is restarted
            {
                xDoc.Load(xmlfile);
                foreach (XmlElement el in xDoc.DocumentElement.ChildNodes)
                {
                    Dynam i = Dynam.FromXml(el);
                    if (i != null)
                    {
                        products.Add(i);
                    }
                }
                if (products == null)
                {
                    Console.WriteLine("File seems to be empty or its structure is wrong");
                }
                Console.WriteLine("Loaded, viewing");
                foreach (Dynam i in products) // shows all loaded data
                {
                    Console.WriteLine(i.Table());
                }
                Console.WriteLine("Do you want to import these recrods to the database ? y/n"); // asks if user would want to import this data into the database
                if (Console.ReadKey().KeyChar == 'y')
                {
                    foreach (Dynam item in products)
                    {
                        data.Add(item);
                    }
                }
            }
            catch (Exception e) // restarts in case of error
            {
                Console.Write(e.Message);
                ViewOther();
            }

            Console.WriteLine("View more files ? y/n :");
            if (Console.ReadKey().KeyChar == 'y')
            {
                ViewOther();
            }
            else
            {
                state = AppState.MainMenu;

            }
        }
        void Save() // Saves base to a file
        {


            using (XmlWriter writer = XmlWriter.Create(baseDataPath)) // Saves base to file according to the current state
            {

                writer.WriteStartDocument();
                writer.WriteStartElement(headNames[Program.curState]);
                foreach (Dynam i in data)
                {
                    i.ToXml(writer);
                }
                writer.WriteEndDocument();
            }
            Journal($"Saved changes to {fileNames[Program.curState]}");
            //newWr.Writ

            return;
        }
        public void SetDestination() // Lets user change default destination to custom path.
        {
            Console.Write($"\n Current destination: {dataPathFolder}");
            Console.Write("Change destination ? y/n");
            if (Console.ReadKey().KeyChar == 'y')
            {
                Console.Write("\n Your new destination: ");
                baseConfigPath = Console.ReadLine();
                Journal($"Changed default destination to:{baseConfigPath}");
            }
            else
            {
                state = AppState.MainMenu;
            }
        }
        void SortByName()  // called when user chooses to sort by name in ListNSort function (1003)
        {
            Console.Write("\n 0 - Exit to main menu 1 - Sort base by name ");
            if (Console.ReadKey().KeyChar == '1')
            {
                List<Dynam> temp = new List<Dynam>();
                foreach (Dynam item in data)
                {
                    temp.Add(item.ToCopy());
                }
                Console.Write($"\n{data[0].Value()}"); // shows available sorting opitons
                bool isValid;
                int sortId = 0;
                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out sortId);
                    if (sortId < 0 || sortId > 6)
                    {
                        isValid = false;
                    }
                } while (!isValid); // check for valid value
                if (sortId == 0)
                {
                    state = AppState.MainMenu;
                    return;

                }
                Console.Write("\n Name:"); // Asks user for string he'd like to sort everything by
                string name = Console.ReadLine();
                Console.Write("\n Sorting: 1) increase, 2) decrease :");
                int sortMeth = 0;
                isValid = false;
                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out sortMeth);
                    if (sortMeth < 0 || sortMeth > 2) { isValid = false; }
                } while (!isValid);

                var props = temp.GetType().GetProperties();

                switch (sortId)
                {
                    case 2:
                        for (int i = 0; i < temp.Count; i++)
                        {

                            if (temp[i].one.GetType() == typeof(string) && temp[i].one != name || (temp[i].one.GetType() == typeof(int) && (temp[i].one != Convert.ToInt32(name))))
                            {
                                temp.RemoveAt(i);
                                i--;
                            }
                        }
                        break;
                    case 3:
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (temp[i].two.GetType() == typeof(string) && temp[i].two != name || temp[i].two.GetType() != typeof(string) && temp[i].two != Convert.ToInt32(name))
                            {
                                temp.RemoveAt(i);
                                i--;
                            }

                        }
                        break;
                    case 4:
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (temp[i].three.GetType() == typeof(string) && temp[i].three != name || temp[i].three.GetType() != typeof(string) && temp[i].three != Convert.ToInt32(name))
                            {
                                temp.RemoveAt(i);
                                i--;
                            }

                        }
                        break;
                    case 5:
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (temp[i].four.GetType() == typeof(string) && temp[i].four != name || temp[i].four.GetType() != typeof(string) && temp[i].four != Convert.ToInt32(name))
                            {
                                temp.RemoveAt(i);
                                i--;
                            }

                        }
                        break;
                    case 6:
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (temp[i].five.GetType() == typeof(string) && temp[i].five != name || temp[i].five.GetType() != typeof(string) && temp[i].five != Convert.ToInt32(name))
                            {
                                temp.RemoveAt(i);
                                i--;
                            }
                        }
                        break;
                } //this one excludes any recordings that do not match the users conditions

                temp = Sort(sortId, temp, sortMeth); // calls for sort method which sorts everything according to users preferences
                Console.WriteLine("\n " + new Dynam(null, null, null, null, null).Header());
                for (int i = 0; i < temp.Count; i++)
                {
                    Console.WriteLine(temp[i].Table());


                }
            }
            else
            {
                state = AppState.MainMenu;
                MainMenu();
            }
        }
        void ListNSort()
        {
            if (!data.Any())
            {
                Console.WriteLine("Database is empty, please insert/import/change structure to fill it");
                state = AppState.MainMenu;
                return;
            }
            XmlDocument doc = new XmlDocument();
            List<Dynam> products = new List<Dynam>(); // copies database to temporary List
            foreach (var item in data)
            {
                products.Add(item);
            }



            Console.Write("\n Do you want to sort data ? y/n");
            if (Console.ReadKey().KeyChar == 'y')
            {

                Console.Write("\n Do you want to sort by name ? y/n :"); // calls SortByName if user wishes so
                if (Console.ReadKey().KeyChar == 'y')
                {
                    SortByName();
                }
                else
                {


                    Console.Write($"\n{data[0].Value()}");
                    bool isValid;
                    int sortId = 0;
                    do
                    {
                        isValid = int.TryParse(Console.ReadLine(), out sortId);
                        if (sortId < 0 || sortId > 6)
                        {
                            isValid = false;
                        }
                    } while (!isValid);
                    if (sortId == 0)
                    {
                        state = AppState.MainMenu;
                        return;

                    }
                    Console.Write("\n Sorting: 1) increase, 2) decrease :");
                    int sortMeth = 0;
                    isValid = false;
                    do
                    {
                        isValid = int.TryParse(Console.ReadLine(), out sortMeth);
                        if (sortMeth < 0 || sortMeth > 2) { isValid = false; }
                    } while (!isValid);

                    List<Dynam> temporary = Sort(sortId, products, sortMeth); // calls for sorting which does all the work udner the entered parametres

                    Console.Write(" " + temporary[0].Header());
                    foreach (Dynam i in temporary) //prints the sorted List
                    {
                        Console.Write($"\n {i.Table()}");
                    }
                }
            }
            else // if user doesnt wish to sort the List it will only shows it
            {
                Console.WriteLine($"\n{data[0].Header()} ");
                foreach (Dynam i in data)
                {
                    Console.WriteLine(i.Table());
                }
            }
            Console.Write("\n 0 -  Exit to main Menu, 1 - sort again");
            if (Console.ReadKey().KeyChar == '1')
            {
                ListNSort();// restarts sorting
            }
            else
            {
                state = AppState.MainMenu;
            }

        }
        void DataModify() // works same as AddingLine() but instead of adding sets the new structure to users index
        {
            Console.Write($"");
            for (int i = 0; i < data.Count; i++)
            {
                Console.WriteLine($"{i}: {data[i].Table()}");
            }
            Console.Write("\nEnter id of recording you'd like to change");
            int changeId = 0;
            int.TryParse(Console.ReadLine(), out changeId);
            // initialzing products name, sellers name, quantity of a product, its price, products date of a sale
            string prn = ""; //product name
            string slrn = ""; //seller's name
            string qnt = ""; //quntity
            string prc = ""; //price
            string dof = ""; //date of sale
                             //part where user needs to type his values

            Console.WriteLine($"\nPlease type field1");
            prn = Console.ReadLine();

            Console.WriteLine("Please type field2");
            slrn = Console.ReadLine();

            Console.WriteLine("Please type field3");
            qnt = Console.ReadLine();

            Console.WriteLine("Please type field4");
            prc = Console.ReadLine();

            Console.WriteLine("Please type field5");
            dof = Console.ReadLine();



            try
            {
                if (Program.curState == 0)
                {
                    int a = 0, b = 0;
                    bool i = int.TryParse(qnt, out a);
                    bool k = int.TryParse(prc, out b);
                    DateTime odate;
                    bool f = DateTime.TryParse(dof, out odate);
                    if (!i || !k || !f)
                    {
                        Console.WriteLine($"Inputted lines {qnt} or {prc} are of wrong type and were set to 0 as default");
                    }
                    data[changeId] = new Product(prn, slrn, a, b, odate.ToString("yyyy-MM-dd")).ToDynam();
                }
                else if (Program.curState == 1)
                {
                    int a = 0, b = 0;
                    bool i = int.TryParse(prn, out a);
                    bool k = int.TryParse(dof, out b);
                    DateTime start;
                    DateTime finish;
                    bool arr = DateTime.TryParse(slrn, out finish);
                    bool st = DateTime.TryParse(qnt, out start);
                    if (!i || !k || !arr || !st)
                    {
                        Console.WriteLine($"Inputted lines {prn}, {dof}, {slrn} or {qnt} are of wrong type and were set to 0 or default value");
                    }
                    data[changeId] = new Train(a, finish.ToString("yyyy-MM-dd"), start.ToString("yyyy-MM-dd"), prc, b).ToDynam();
                }
                else if (Program.curState == 2)
                {
                    int a = 0;
                    bool i = int.TryParse(prc, out a);
                    DateTime prodTime;
                    bool st = DateTime.TryParse(dof, out prodTime);
                    if (!i || !st)
                    {
                        Console.WriteLine($"Inputted line/lines {dof} or {prc} are of wrong type and were set to 0 or default value");
                    }
                    data[changeId] = new Cars(prn, slrn, qnt, a, prodTime.ToString("yyyy-MM-dd")).ToDynam();
                }
                else if (Program.curState == 3)
                {
                    DateTime tempor;
                    bool st = DateTime.TryParse(dof, out tempor);
                    if (!st)
                    {
                        Console.WriteLine($"Inputted line {dof} is of wrong type and were set to 0 or default value");
                    }
                    data[changeId] = new Information(prn, slrn, qnt, prc, tempor.ToString("yyyy-MM-DD")).ToDynam();
                }
                Journal($"Changed record in the database udner index{changeId}");
            }
            catch (Exception a)// if user types wrong information (for example instead of sellers name he typed number) "catch part will notify user that mistake was made"
            {
                Console.WriteLine("Something caused a problem! Here's the error message: {0}" + a.Message);
            }
            state = AppState.MainMenu;
        }
        void Export() // 
        {
            char c;
            string locPath = " ";
            Console.Write("\n Type 1 - Your own file destination 2 - default 0 - exit to main menu : ");
            do
            {
                c = Console.ReadKey().KeyChar;
            } while (!(c == '1' || c == '2' || c == '0'));
            if (c == '1')
            {
                Console.Write("\n Enter your destination:");
                locPath = Console.ReadLine();
                Console.Write("\n Enter name of your file (.xml will be added automatically): ");
                string name = Console.ReadLine();
                locPath = locPath + name + ".xml";

            }
            else if (c == '2')
            {
                Console.Write("\n Using default (%APPDATA%) destination");
                Console.Write("\n Enter name of your file (.xml will be added automatically): ");
                string name = Console.ReadLine();
                locPath = dataPathFolder + name + ".xml";
            }
            else if (c == '0')
            {
                state = AppState.MainMenu;
                MainMenu();
                return;
            }

            using (XmlWriter wr = XmlWriter.Create(locPath))
            {

                wr.WriteStartDocument();
                wr.WriteStartElement("Fields"); // writes all values to a new xml file

                foreach (Dynam i in data)
                {
                    wr.WriteStartElement("Field");

                    wr.WriteElementString("Field1", dynToString(i.one));
                    wr.WriteElementString("Field2", dynToString(i.two));
                    wr.WriteElementString("Field3", dynToString(i.three));
                    wr.WriteElementString("Field4", dynToString(i.four));
                    wr.WriteElementString("Field5", dynToString(i.five));

                    wr.WriteEndElement();
                }
                wr.WriteEndDocument();
            }
            Journal($"Exported current database to {locPath}");

        }
        void Journal(string operation) // gets the operation string and writes it to log with the DateTime.Now
        {
            DateTime now = DateTime.Now;
            File.AppendAllText(dataPathFolder + "journal.log", $"[{now.Hour}:{now.Minute}:{now.Second}] {operation}" + Environment.NewLine);
        }
        public string dynToString(dynamic value) //just converts any value to string. Used in the export method
        {
            if (value.GetType() == typeof(int))
            {
                return value.ToString();
            }
            else
            {
                return value;
            }
        }
        List<Dynam> Sort(int sortid, List<Dynam> n, int sortMeth) // Sorts everything based on data input from the caller
        {
            var array = new dynamic[n.Count];
            switch (sortid) // isolates the column that is supposed to be searched by
            {

                case 2:
                    for (int i = 0; i < n.Count; i++)
                    {
                        array[i] = n[i].one;
                    }
                    break;
                case 3:
                    for (int i = 0; i < n.Count; i++)
                    {
                        array[i] = n[i].two;
                    }
                    break;
                case 4:
                    for (int i = 0; i < n.Count; i++)
                    {
                        array[i] = n[i].three;
                    }
                    break;
                case 5:
                    for (int i = 0; i < n.Count; i++)
                    {
                        array[i] = n[i].four;
                    }
                    break;
                case 6:
                    for (int i = 0; i < n.Count; i++)
                    {
                        array[i] = n[i].five;
                    }
                    break;
            }



            for (int i = 0; i < n.Count; i++)
            {
                if (sortMeth == 1) // Increase/Decrease 
                {
                    DateTime d;
                    for (int k = 0; k < n.Count; k++)
                    {
                        int a = 0, b = 0;
                        if (array[i].GetType() == typeof(int)) // checks for the type, then sorts
                        {

                            if (array[i] < array[k])
                            {
                                var temp = array[i];
                                array[i] = array[k];
                                array[k] = temp;

                                Dynam tempP = n[i];
                                n[i] = n[k];
                                n[k] = tempP;
                            }
                        }
                        else if (DateTime.TryParse(array[i], out d)) // if string can be parsed to DateTime it means that its date and will be sorted accordingly
                        {
                            DateTime da;
                            DateTime db;
                            DateTime.TryParse(array[i], out da);
                            DateTime.TryParse(array[k], out db);
                            if (DateTime.Compare(da, db) < 0)
                            {
                                var temp = array[i];
                                array[i] = array[k];
                                array[k] = temp;

                                Dynam tempP = n[i];
                                n[i] = n[k];
                                n[k] = tempP;
                            }

                        }
                        else if (array[i].GetType() == typeof(string))
                        {
                            a = array[i].Length;
                            b = array[k].Length;
                            if (a < b)
                            {
                                var temp = array[i];
                                array[i] = array[k];
                                array[k] = temp;

                                Dynam tempP = n[i];
                                n[i] = n[k];
                                n[k] = tempP;
                            }
                        }
                    }
                }
                if (sortMeth == 2) // Increase/Decrease
                {
                    DateTime d;
                    for (int k = 0; k < n.Count; k++)
                    {
                        int a = 0, b = 0;
                        if (array[i].GetType() == typeof(int))
                        {

                            if (array[i] > array[k])
                            {
                                var temp = array[i];
                                array[i] = array[k];
                                array[k] = temp;

                                Dynam tempP = n[i];
                                n[i] = n[k];
                                n[k] = tempP;
                            }
                        }
                        else if (DateTime.TryParse(array[i], out d))
                        {
                            DateTime da;
                            DateTime db;
                            DateTime.TryParse(array[i], out da);
                            DateTime.TryParse(array[k], out db);
                            if (DateTime.Compare(da, db) > 0)
                            {
                                var temp = array[i];
                                array[i] = array[k];
                                array[k] = temp;

                                Dynam tempP = n[i];
                                n[i] = n[k];
                                n[k] = tempP;
                            }

                        }
                        else if (array[i].GetType() == typeof(string))
                        {
                            a = array[i].Length;
                            b = array[k].Length;
                            if (a > b)
                            {
                                var temp = array[i];
                                array[i] = array[k];
                                array[k] = temp;

                                Dynam tempP = n[i];
                                n[i] = n[k];
                                n[k] = tempP;
                            }
                        }

                    }
                }

            }
            return n;
        }
    }
}


