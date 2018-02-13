using System;
using System.Collections.Generic;

namespace DesignPatternsLearning.Structural
{

    /*
     Source:http://www.dofactory.com/net/decorator-design-pattern
     
     Definition

        Attach additional responsibilities to an object dynamically. Decorators provide a flexible alternative to subclassing for extending functionality.
     
     Participants

    The classes and objects participating in this pattern are:

    Component   (LibraryItem)
        defines the interface for objects that can have responsibilities added to them dynamically.
    ConcreteComponent   (Book, Video)
        defines an object to which additional responsibilities can be attached.
    Decorator   (Decorator)
        maintains a reference to a Component object and defines an interface that conforms to Component's interface.
    ConcreteDecorator   (Borrowable)
        adds responsibilities to the component.
    */
    class DecoratorPattern
    {
        public static void Run()
        {
            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();

            d1.SetComponent(c);
            d2.SetComponent(d1);

            d2.Operation();


            // Create book

            Book book = new Book("Worley", "Inside ASP.NET", 10);
            book.Display();

            // Create video

            Video video = new Video("Spielberg", "Jaws", 92);
            video.Display();

            // Make video borrowable, then borrow and display

            Console.WriteLine("\nMaking video borrowable:");

            Borrowable borrowvideo = new Borrowable(video);
            borrowvideo.BorrowItem("Customer #1");
            borrowvideo.BorrowItem("Customer #2");

            borrowvideo.Display();

        }
    }

    #region StructrualCode
    abstract class Component
    {
        public abstract void Operation();
    }

    class ConcreteComponent : Component
    {
        public override void Operation()
        {
            Console.WriteLine("Concrete Component Operation");
        }
    }

    abstract class Decorator : Component
    {
        protected Component component;

        public override void Operation()
        {
            if (component != null)
                component.Operation();
        }

        public void SetComponent(Component c)
        {
            this.component = c;
        }
    }

    class ConcreteDecoratorA : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            Console.WriteLine("Concrete Decorator A Operation");
        }
    }

    class ConcreteDecoratorB : Decorator
    {
        public override void Operation()
        {
            base.Operation();
            AddedBehavior();
            Console.WriteLine("Concrete Decorator B Operation");
        }

        void AddedBehavior()
        {
        }
    }

    #endregion

    #region Real-World Code

    abstract class LibraryItem
    {
        public int NumberOfCopies { get; set; }

        public abstract void Display();
    }

    class Book : LibraryItem
    {
        public string Author { get; set; }
        public string Title { get; set; }
        public Book(string author, string title, int count)
        {
            this.Author = author;
            this.Title = title;
            this.NumberOfCopies = count;
        }

        public override void Display()
        {
            Console.WriteLine("Book Details");
            Console.WriteLine("Author:{0}, Title:{1}, Number of Copies:{2}", Author, Title, NumberOfCopies);
        }
    }

    class Video : LibraryItem
    {
        public string Director { get; set; }
        public string Title { get; set; }
        public Video(string director, string title, int count)
        {
            this.Director = director;
            this.Title = title;
            this.NumberOfCopies = count;
        }

        public override void Display()
        {
            Console.WriteLine("Video Details");
            Console.WriteLine("Director:{0}, Title:{1}, Number of Copies:{2}", Director, Title, NumberOfCopies);
        }
    }

    abstract class LibDecorator : LibraryItem
    {
        protected LibraryItem libraryItem;

        public LibDecorator(LibraryItem libraryItem)
        {
            this.libraryItem = libraryItem;
        }

        public override void Display()
        {
            libraryItem.Display();
        }
    }

    class Borrowable : LibDecorator
    {
        protected List<string> borrowers = new List<string>();

        // Constructor

        public Borrowable(LibraryItem libraryItem) : base(libraryItem)
        {
        }

        public void BorrowItem(string name)
        {
            borrowers.Add(name);
            libraryItem.NumberOfCopies--;
        }

        public void ReturnItem(string name)
        {
            borrowers.Remove(name);
            libraryItem.NumberOfCopies++;
        }

        public override void Display()
        {
            base.Display();

            foreach (string borrower in borrowers)
            {
                Console.WriteLine(" borrower: " + borrower);
            }
        }
    }

    #endregion
}
