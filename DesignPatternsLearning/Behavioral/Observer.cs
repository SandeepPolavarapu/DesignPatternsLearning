using System;
using System.Collections.Generic;

namespace DesignPatternsLearning.Behavioral
{

    /*
    Source: http://www.dofactory.com/net/observer-design-pattern
    
    Definition

        Define a one-to-many dependency between objects so that when one object changes state, all its dependents are notified and updated automatically.
    
    Participants

    The classes and objects participating in this pattern are:
    
    Subject  (Stock)
        knows its observers. Any number of Observer objects may observe a subject
        provides an interface for attaching and detaching Observer objects.
    ConcreteSubject(IBM)
        stores state of interest to ConcreteObserver
        sends a notification to its observers when its state changes
    Observer(IInvestor)
        defines an updating interface for objects that should be notified of changes in a subject.
    ConcreteObserver(Investor)
        maintains a reference to a ConcreteSubject object
        stores state that should stay consistent with the subject's
        implements the Observer updating interface to keep its state consistent with the subject's

    */
    static class ObserverPattern
    {
        public static void Run()
        {
            ConcreteSubject s = new ConcreteSubject();
            s.Attach(new ConcreteObserver(s, "O1"));
            s.Attach(new ConcreteObserver(s, "O2"));
            s.Attach(new ConcreteObserver(s, "O3"));
            s.SubjectState = "XYZ";
            s.Notify();

            IBM ibm = new IBM("IBM", 120.00);
            ibm.Attach(new Investor("Sorros"));
            ibm.Attach(new Investor("Berkshire"));

            // Fluctuating prices will notify investors

            ibm.Price = 120.10;
            ibm.Price = 121.00;
            ibm.Price = 120.50;
            ibm.Price = 120.75;
        }
    }

    #region StructuralCode
    public abstract class Subject
    {
        private List<Observer> observers = new List<Observer>();
        public void Attach(Observer o)
        {
            observers.Add(o);
        }

        public void Detach(Observer o)
        {
            observers.Remove(o);
        }

        public void Notify()
        {
            foreach (Observer o in observers)
            {
                o.Update();
            }
        }
    }

    public class ConcreteSubject : Subject
    {
        public string SubjectState { get; set; }
    }

    public abstract class Observer
    {
        public abstract void Update();
    }

    public class ConcreteObserver : Observer

    {
        private string _name;
        private string _observerState;
        private ConcreteSubject _subject;

        // Constructor

        public ConcreteObserver(
          ConcreteSubject subject, string name)
        {
            this._subject = subject;
            this._name = name;
        }

        public override void Update()
        {
            _observerState = _subject.SubjectState;
            Console.WriteLine("Observer {0}'s new state is {1}",
              _name, _observerState);
        }

        // Gets or sets subject

        public ConcreteSubject Subject
        {
            get { return _subject; }
            set { _subject = value; }
        }
    }
    #endregion

    #region Real-World Code

    public abstract class Stock
    {
        public string Symbol { get; set; }

        private double _price;

        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                _price = value;
                Notify();
            }
        }

        public List<IInvestor> investors = new List<IInvestor>();

        public Stock(string symbol, double price)
        {
            this.Symbol = symbol;
            this._price = price;
        }

        public void Attach(IInvestor i)
        {
            investors.Add(i);
        }

        public void Detach(IInvestor i)
        {
            investors.Remove(i);
        }

        public void Notify()
        {
            foreach (IInvestor i in investors)
            {
                i.Update(this);
            }
        }
    }

    public class IBM : Stock
    {
        public IBM(string symbol, double price) : base(symbol, price)
        {

        }

    }

    public interface IInvestor
    {
        void Update(Stock stock);
    }

    public class Investor : IInvestor
    {
        public string Name { get; set; }

        public Investor(string name)
        {
            this.Name = name;
        }
        public void Update(Stock stock)
        {
            Console.WriteLine("Notified {0} about stock {1} price is {2}", Name, stock.Symbol, stock.Price);
        }
    }

    #endregion

}
