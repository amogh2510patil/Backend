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
                if(customer == null)
                {
                    return 0;
                }
                if (transaction.type == "W")
                {
                    customer.balance -= transaction.amount;
                }
                else if(transaction.type == "D")
                {
                    customer.balance += transaction.amount;
                }
                else if(transaction.type == "F")
                {
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
