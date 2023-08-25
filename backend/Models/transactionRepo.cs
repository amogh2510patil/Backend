using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
            interface Itransaction
        {
            List<transaction> Gettransaction(int accNo);
            bool Deletetransaction(int accNo);
            int Inserttransaction(transaction transaction);
            int Updatetransaction(transaction transaction);
        int InsertCheque(cheque cheque);
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

        public List<cheque> GetCheque(int accNo)
        {
            //using (var db = new transactionDbContext())
            {
                List<cheque> chequelist = new List<cheque>();
                chequelist = db.cheque.Where(c => c.accno == accNo).ToList();
                return chequelist;
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
        public int InsertCheque(cheque cheque)
        {
            customer customer = db.customer.Find(cheque.accno);
            db.cheque.Add(cheque);

            db.SaveChangesAsync();
            return 1;
            //return 0;
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
                    if (transaction.amount > customer.balance) { return 0; }
                    transaction.currency = "";
                    transaction.recipient = 0;
                    customer.balance -= transaction.amount;
                }
                else if(transaction.type == "D" || transaction.type == "Chq")
                {
                    transaction.currency = "";
                    transaction.recipient = 0;
                    customer.balance += transaction.amount;
                }
                else if(transaction.type == "F")
                {
                    if (transaction.amount > customer.balance) { return 0; }
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
                        if ((80 * transaction.amount) > customer.balance) { return 0; }
                        customer.balance -= (80 * transaction.amount);
                    }
                    else if (transaction.currency == "Pound")
                    {
                        if ((105 * transaction.amount) > customer.balance) { return 0; }
                        customer.balance -= (105 * transaction.amount);
                    }
                    else if (transaction.currency == "Euro")
                    {
                        if ((90 * transaction.amount) > customer.balance) { return 0; }
                        customer.balance -= (90 * transaction.amount);
                    }
                    else if (transaction.currency == "Yen")
                    {
                        if ((0.5 * transaction.amount) > customer.balance) { return 0; }
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
