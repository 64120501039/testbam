using System;
namespace bamtest
{
	public class ProgressionClass
	{
		public static void CheckParticipant(string name, string surname, SomeCompany.Account acc)
		{
			string dictionaryKeys = name + surname;

			if (SomeCompany.RegisteredData.ParticipantDictionary.ContainsKey(dictionaryKeys))
			{
				Console.WriteLine("User is already registered. Please try again.");

				RecieveInput.RecieveName(acc);
			}
		}

		public static void CheckParticipantEmail(string email, SomeCompany.Account acc)
		{
			foreach (var item in SomeCompany.RegisteredData.ParticipantDictionary)
			{
				if (item.Value.accEmail == email || email == "exit")
				{
					Console.WriteLine("Invalid email. Please try again.");

					RecieveInput.RecieveEmail(acc);
				}


				break;
			}
		}

		public static void CheckParticipantPassword(string password, string cfpassword, SomeCompany.Account acc)
		{
			if (password == cfpassword) return;
			else
			{
				Console.WriteLine("Mismatched password. Please try again.");
				RecieveInput.RecievePassword(acc);
			}

		}

		public static void CheckNameTitle(string title, SomeCompany.Account acc)
        {
			if (title == "Mrs" || title == "Miss" || title == "Ms") return;

			else
			{
				Console.WriteLine("Please input only  Mrs / Miss / Ms");
				RecieveInput.RecieveTitle(acc);
			}
        }

		public static void VerifyEmailandPassword(string email ,string password)
        {
			if(email != "exit")
            {
				if (!SomeCompany.RegisteredData.RegisteredDictionary.ContainsKey(email))
				{
					Console.WriteLine("Incorrect Email or Password. Please try again");
					Menu.DisplayLogin();
				}
				else
				{
					string pss = SomeCompany.RegisteredData.RegisteredDictionary[email].accCfPassword;

					if (password != pss)
					{
						Console.WriteLine("Incorrect Email or Password. Please try again");
						Menu.DisplayLogin();
					}
				}
            }
            else
            {
				Menu.DisplayMainMenu();
			}
        }
	}

	public class RecieveInput
	{
		public static void RecieveTitle(SomeCompany.Account acc)
        {
			Console.WriteLine("Enter your name title");
			string title = Console.ReadLine();

			ProgressionClass.CheckNameTitle(title,acc);

			acc.accTitle = title;
		}

		public static void RecieveName(SomeCompany.Account acc)
		{
			Console.WriteLine("Enter your name");
			string name = Console.ReadLine();

			RecieveSurname(name,acc);
		}

		public static void RecieveSurname(string name, SomeCompany.Account acc)
		{
			Console.WriteLine("Enter your surname ");
			string surname = Console.ReadLine();

			ProgressionClass.CheckParticipant(name,surname,acc);

			acc.accName = name;
			acc.accSurname = surname;
		}

		public static void RecieveAge(SomeCompany.Account acc)
		{
			Console.WriteLine("Enter your age");
			int age = int.Parse(Console.ReadLine());

			acc.accAge = age;
		}

		public static void RecieveEmail(SomeCompany.Account acc)
		{
			Console.WriteLine("Enter your email");
			string email = Console.ReadLine();

			ProgressionClass.CheckParticipantEmail(email,acc);

			acc.accEmail = email;

		}

		public static void RecieveStudentId(SomeCompany.Account acc)
		{
			Console.WriteLine("Enter your StudentId");
			string id = Console.ReadLine();

			acc.accStudentId = id;

		}

		public static void RecievePassword(SomeCompany.Account acc)
		{
			Console.WriteLine("Enter your password");
			string password = Console.ReadLine();

			RecieveCfPassword(password, acc);

		}

		public static void RecieveCfPassword(string password, SomeCompany.Account acc)
		{
			Console.WriteLine("Confirme your password");
			string cfpassword = Console.ReadLine();

			ProgressionClass.CheckParticipantPassword(password, cfpassword,acc);

			acc.accPassword = password;
			acc.accCfPassword = cfpassword;

		}
	}

	public class Menu
    {
		public static SomeCompany.Account thisccount;

		public static void DisplayMainMenu()
        {
			string programMode;

			Console.Clear();

			Console.WriteLine("Menu"); //adjust to whatever u want
			Console.WriteLine("Please type 1 to Register or  type 2 to Login"); //adjust to whatever u want
			programMode = Console.ReadLine();

			OnSelectedMenu(programMode);

		}

		public static void DisplayLogin()
        {
			Console.WriteLine("Please enter your Email.");
			string email = Console.ReadLine();
			Console.WriteLine("Please enter your Password.");
			string password = Console.ReadLine();

			ProgressionClass.VerifyEmailandPassword(email, password);
			thisccount = SomeCompany.RegisteredData.GetAccByEmail(email);
			OnloginSuccess();

		}

		public static void OnSelectedMenu(string programMode)
        {
            switch (programMode)
            {
                default:
                    break;

				case "1":

					Console.Clear();
					Console.WriteLine("Register Menu");
					Console.WriteLine("Please type 1 to Register in general or type 2 to Register in student"); //adjust to whatever u want

					SomeCompany.Account newAccount = new SomeCompany.Account();

					programMode = Console.ReadLine();

					if (programMode == "1") // general
					{
						RecieveInput.RecieveTitle(newAccount);
						RecieveInput.RecieveName(newAccount);

						RecieveInput.RecieveAge(newAccount);
						RecieveInput.RecieveEmail(newAccount);
						RecieveInput.RecievePassword(newAccount);

						newAccount.participantType = "general";

					}
					if (programMode == "2") //student 
					{
						RecieveInput.RecieveTitle(newAccount);
						RecieveInput.RecieveName(newAccount);

						RecieveInput.RecieveAge(newAccount);
						RecieveInput.RecieveEmail(newAccount);
						RecieveInput.RecieveStudentId(newAccount);
						RecieveInput.RecievePassword(newAccount);

						newAccount.participantType = "student";

					}

					SomeCompany.RegisteredData.ParticipantDictionary.Add(newAccount.accName + newAccount.accSurname, newAccount);
					SomeCompany.RegisteredData.RegisteredDictionary.Add(newAccount.accEmail, newAccount);

					Console.Clear();

					DisplayMainMenu();

					break;

				case "2":

					DisplayLogin();

					break;
			}

		}

		public static void OnloginSuccess() /////// not done
        {
			string mode;
			
			Console.Clear();
            Console.WriteLine("Reserve Seat type 1");
			Console.WriteLine("Check reserve status type 2");
			Console.WriteLine("Logout type 0");

			mode = Console.ReadLine();

            switch (mode)
            {
                default:
                    break;

				case "1":
					ReserveSeat.OnResreveSeat();
					break;
				case "2":
					ReserveResult.DisplayReserveResult();
					break;
				case "0":
					Menu.DisplayMainMenu();
					break;
			}
		}
    }

	public class ReserveSeat
    {
		public static SomeCompany.Account thisaccount = Menu.thisccount;
		public static List<string> prefredSeat = new List<string>();

		public static string PrefreSeat;
      
		public static void DisplayPreferedSeats()
        {
            Console.WriteLine("Prefer seats : " + PrefreSeat);
        }

		public static void OnResreveSeat()
        {
			Console.WriteLine("Reserve Seat");
			DisplayPreferedSeats();
			Console.WriteLine("Plaese enter your seat number (example A1 or B5) #Please type in Upper letter");
			string seat = Console.ReadLine();

			VerifySeat(seat);

			Console.Write("Please enter checkout to checkout");
			Console.ReadLine();
        }

		public static void OnSelecSeats()
        {
			DisplayPreferedSeats();
			Console.WriteLine("Plaese enter your seat number");
			string seat = Console.ReadLine();
			VerifySeat(seat);
		}

		public static void VerifySeat(string seatnumber)
        {
			if (seatnumber != "exit")
            {
                if (seatnumber != "checkout")
                {
					if(seatnumber.Contains("A"))
					{
						if (thisaccount.participantType == "student")
						{
							Console.WriteLine("Cannot book. Please try again");
							OnSelecSeats();
						}
						else
						{
							if (!SomeCompany.RegisteredData.ReservedSeats.Contains(seatnumber))
							{
								if (prefredSeat.Contains(seatnumber))
								{
									Console.WriteLine("Already book. Please try again");
									OnSelecSeats();
								}
                                else
                                {
									PrefreSeat += " " + seatnumber;
									prefredSeat.Add(seatnumber);
									OnSelecSeats();
								}
							}
                            else
                            {
								Console.WriteLine("Already book. Please try again");
								OnSelecSeats();
							}
						}
					}
					else
					{
						PrefreSeat += " " + seatnumber;
						prefredSeat.Add(seatnumber);
						OnSelecSeats();
					}
                }
                else
                {
					CheckOuts.allSeat = prefredSeat;
					CheckOuts.DisPlayCheckOutMenu();
				}
            }
            else
            {
				prefredSeat.Clear();
				Menu.DisplayMainMenu();
            }
        }
    }

	public class CheckOuts
    {
		public static SomeCompany.Account thisacc = Menu.thisccount;
		public static List<string> allSeat;
		public static string preferedSeatA;
		public static string preferedSeatB;

		public static float CalculatePrice(int seatQty, string participantType)
        {
			float value = 0;

			float studentprice = 1200.5f;
			float generalprice = 5235.25f;

			if(participantType == "student")
            {
				value = seatQty * studentprice;
            }
            else
            {
				value = seatQty * generalprice;
			}

			return value;
        }

		public static void SplitSeat(List<string> seats)
        {
            foreach (var item in seats)
            {
                if (item.Contains("A"))
                {
					preferedSeatA += " " + item;

				}
                else
                {
					preferedSeatB += " " + item;
				}
            }
        }

		public static void DisPlayCheckOutMenu()
        {
			Console.Clear();

			SplitSeat(allSeat);

            Console.WriteLine("CheckOut Menu");
            Console.WriteLine("Prefre Seats type A : " + preferedSeatA);
			Console.WriteLine("Prefre Seats type B : " + preferedSeatB);
			Console.WriteLine(allSeat.Count + " Seats" + "Total price : " + CalculatePrice(allSeat.Count, thisacc.participantType)) ;

			DisplayCheckOutMenu();
		}

		public static void DisplayCheckOutMenu()
        {
			string mode;

            Console.WriteLine("Pay by Bank. Please type 1");
			Console.WriteLine("Pay by Credit card. Please type 2");
			Console.WriteLine("Cancle. Please type 0");

			mode = Console.ReadLine();
			CheckOutMenuMode(mode);

		}

		public static void CheckOutMenuMode(string mode) 
        {
            switch (mode)
            {
                default:
                    Console.WriteLine("Invalid type. Please try again.");
					DisPlayCheckOutMenu();

					break;

				case "1":
					thisacc.ispaybyBank = true;
					DisplayPayByBank();
					ConfirmedReserve();
					ReserveResult.DisplayReserveResult();
					break;

				case "2":
					DisplayPayByCreditCard();
					ConfirmedReserve();
					ReserveResult.DisplayReserveResult();
					break;

				case "0":

					Console.Clear();
					allSeat.Clear();
					Menu.DisplayMainMenu();

					break;
			}
        }

		public static void DisplayPayByBank()
        {
            Console.WriteLine("Please enter Bank holder name");
			string bankHolder = Console.ReadLine();
            Console.WriteLine("Please enter Bank account");
			string bankAcc = Console.ReadLine();

			Menu.thisccount.mybank.bankHolder = bankHolder;
			Menu.thisccount.mybank.bankAcc = bankAcc;
		}

		public static void DisplayPayByCreditCard()
		{
			Console.WriteLine("Please enter Card holder name");
			string cardHolder = Console.ReadLine();
			Console.WriteLine("Please enter Card number");
			string cardNO = Console.ReadLine();
			Console.WriteLine("Please enter Card expire date");
			string expDate = Console.ReadLine();
			Console.WriteLine("Please enter CVV");
			string CVV = Console.ReadLine();

			Menu.thisccount.mycreditcard.cardHolder = cardHolder;
			Menu.thisccount.mycreditcard.cardNO = cardNO;
			Menu.thisccount.mycreditcard.expDate = expDate;
			Menu.thisccount.mycreditcard.CVV = CVV;
		}

		public static void ConfirmedReserve()
        {
            foreach (var item in allSeat)
            {
				SomeCompany.RegisteredData.ReservedSeats.Add(item);
				thisacc.ReserveredSeats.Add(item);

			}
        }
	}

	public class ReserveResult
    {
		public static SomeCompany.Account thisacc = Menu.thisccount;

		public static void DisplayReserveResult()
        {
			Console.Clear();

			if(thisacc.ReserveredSeats.Count <= 0)
                Console.WriteLine("Please book ypur seat first.");
            else
            {
				Console.WriteLine("Participant type : " + thisacc.participantType);
				Console.WriteLine("Reserved Seat TypeA : " + CheckOuts.preferedSeatA);
				Console.WriteLine("Reserved Seat TypeB : " + CheckOuts.preferedSeatB);
				Console.WriteLine(CheckOuts.allSeat.Count + " Seats" + "Total price : " + CheckOuts.CalculatePrice(CheckOuts.allSeat.Count, thisacc.participantType));


				if (thisacc.ispaybyBank)
				{
					Console.WriteLine("Pay by Bank");
					Console.WriteLine(" Bank holder Name : " + thisacc.mybank.bankHolder);
					Console.WriteLine(" Bank Account : " + thisacc.mybank.bankAcc);
				}
				else
				{
					Console.WriteLine("Pay by CreditCard");
					Console.WriteLine(" CreditCard holder Name : " + thisacc.mycreditcard.cardHolder);
					Console.WriteLine(" CreditCard Number : " + thisacc.mycreditcard.cardNO);
					Console.WriteLine(" CreditCard expire date : " + thisacc.mycreditcard.expDate);
					Console.WriteLine(" CreditCard CVV : " + thisacc.mycreditcard.CVV);
				}
			}
            

			Console.WriteLine("Please enter any to continue");
			Console.ReadLine();

			Menu.DisplayMainMenu();

		}

		public static void CheckOut()
        {
			Console.WriteLine("Please enter any to continue");
			string mode = Console.ReadLine();

			if(mode != "checkout")
            {
				CheckOut();

			}
		}
    }
}