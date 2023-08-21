using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
            interface Itransaction
        {
            List<transaction> Gettransaction(int accNo);
            bool Deletetransaction(int accNo);
            int Inserttransaction(transaction transaction);
            int Updatetransaction(transaction transaction);
        }

        public class transactionRepo : Itransaction
        {
            private readonly customerDbContext db;

            public transactionRepo(customerDbContext _transactionDbContext)
            {
                db = _transactionDbContext;
            }

            public List<transaction> Gettransaction(int accNo)
            {
                //using (var db = new transactionDbContext())
                {
                List<transaction> transactionList = new List<transaction>();
                transactionList = db.transaction.Where(t => t.accountnum == accNo || t.recipient==accNo).ToList();
                return transactionList;
            }
            }
        public List<transaction> GetStatement(int accNo,DateTime startDate, DateTime endDate)
        {
            //using (var db = new transactionDbContext())
            {
                List<transaction> transactionList = new List<transaction>();
                transactionList = db.transaction.Where(t => (t.accountnum == accNo || t.recipient == accNo) && (t.dateTime>=startDate && t.dateTime<=endDate)).ToList();
                return transactionList;
            }
        }

        public bool Deletetransaction(int transactionnum)
            {
                //using (var db = new transactionDbContext())
                {
                    bool a = false;
                    var transaction = db.transaction.Find(transactionnum);
                    if (transaction != null)
                    {
                        a = true;
                        db.transaction.Entry(transaction).State = EntityState.Deleted;
                        db.SaveChanges();

                    }
                    else
                    {
                        a = false;
                    }
                    return a;
                }
            }

            public int Inserttransaction(transaction transaction)
            {
                //using (var db = new transactionDbContext())
                {
                customer customer = db.customer.Find(transaction.accountnum);
                transaction.dateTime = DateTime.Now;
                if(customer == null)
                {
                    return 0;
                }
                if (transaction.type == "W")
                {
                    transaction.currency = "";
                    transaction.recipient = 0;
                    customer.balance -= transaction.amount;
                }
                else if(transaction.type == "D")
                {
                    transaction.currency = "";
                    transaction.recipient = 0;
                    customer.balance += transaction.amount;
                }
                else if(transaction.type == "F")
                {
                    transaction.currency = "";
                    customer rec = db.customer.Find(transaction.recipient);
                        if(rec == null)
                    {
                        return 0;
                    }
                    else
                    {
                        customer.balance -= transaction.amount;
                        rec.balance += transaction.amount;
                    }
                }
                else if(transaction.type == "CED")
                {
                    transaction.recipient = 0;
                    if (transaction.currency == "Dollar")
                    {
                        customer.balance += (80 * transaction.amount);
                    }
                    else if(transaction.currency == "Pound")
                    {
                        customer.balance += (105 * transaction.amount);
                    }
                    else if(transaction.currency == "Euro")
                    {
                        customer.balance += (90 * transaction.amount);
                    }
                    else if(transaction.currency == "Yen")
                    {
                        customer.balance += ( 0.5 * transaction.amount);
                    }
                }
                else if (transaction.type == "CEW")
                {
                    transaction.recipient = 0;
                    if (transaction.currency == "Dollar")
                    {
                        customer.balance -= (80 * transaction.amount);
                    }
                    else if (transaction.currency == "Pound")
                    {
                        customer.balance -= (105 * transaction.amount);
                    }
                    else if (transaction.currency == "Euro")
                    {
                        customer.balance -= (90 * transaction.amount);
                    }
                    else if (transaction.currency == "Yen")
                    {
                        customer.balance -= (0.5 * transaction.amount);
                    }
                }
                else { return 0; }                                                                                                                                                                                                                                                                                    
                //transaction.customer = customer;
                    db.transaction.Add(transaction);                                                                                                                                                                                                                                                    

                    db.SaveChangesAsync();
                    return 1;
                }

            }

        /*public int FundTransfer(transaction transaction)
        {
            //using (var db = new transactionDbContext())
            {
                customer customer = db.customer.Find(transaction.accountnum);
                customer rec = db.customer.Find(transaction.recipient);
                if (customer == null || rec==null)
                {
                    return 0;
                }
                transaction.type = "F";
                customer.balance -= transaction.amount;
                rec.balance += transaction.amount;
                //transaction.customer = customer;
                db.transaction.Add(transaction);

                db.SaveChangesAsync();
                return 1;
            }
        }*/

            public int Updatetransaction(transaction transactionup)
            {
                //using (var db = new transactionDbContext())
                {
                    db.Entry(transactionup).State = EntityState.Modified;
                    db.SaveChangesAsync();
                    return 1;
                }
            }
        }

}
