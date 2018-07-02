using System;
using System.Collections.Generic;

namespace DesignPatternsLearning.Creational
{
    #region Factory Method Pattern


    /*
    Source:http://www.dofactory.com/net/factory-method-design-pattern
    
    Definition
        Define an interface for creating an object, but let subclasses decide which class to instantiate. Factory Method lets a class defer instantiation to subclasses.
    
    The classes and objects participating in this pattern are:

    Product  (Page)
        defines the interface of objects the factory method creates
    ConcreteProduct  (SkillsPage, EducationPage, ExperiencePage)
        implements the Product interface
    Creator  (Document)
        declares the factory method, which returns an object of type Product. Creator may also define a default implementation of the factory method that returns a default ConcreteProduct object.
        may call the factory method to create a Product object.
    ConcreteCreator  (Report, Resume)
        overrides the factory method to return an instance of a ConcreteProduct.
    */
    class FactoryMethod
    {
        public static void Run()
        {
            Creator[] creators = new Creator[2];

            creators[0] = new ConcreteCreatorA();
            creators[1] = new ConcreteCreatorB();

            foreach (Creator creator in creators)
            {
                Product product = creator.FactoryMethod();
                Console.WriteLine("Created {0}", product.GetType().Name);
            }

            Document[] documents = new Document[2];

            documents[0] = new Resume();
            documents[1] = new Report();

            // Display document pages

            foreach (Document document in documents)
            {
                Console.WriteLine("\n" + document.GetType().Name + "--");
                foreach (Page page in document.Pages)
                {
                    Console.WriteLine(" " + page.GetType().Name);
                }
            }
        }
    }

    #region Structural Code
    abstract class Product
    {

    }

    class ConcreteProductA : Product
    {

    }

    class ConcreteProductB : Product
    {

    }

    abstract class Creator
    {
        public abstract Product FactoryMethod();
    }

    class ConcreteCreatorA : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProductA();
        }
    }
    class ConcreteCreatorB : Creator
    {
        public override Product FactoryMethod()
        {
            return new ConcreteProductB();
        }
    }
    #endregion

    #region Real-World Code

    abstract class Page
    {

    }

    class SkillsPage : Page
    {

    }
    class EducationPage : Page
    {

    }
    class ExperiencePage : Page
    {

    }
    class IntroductionPage : Page
    {

    }
    class BibliographyPage : Page
    {

    }

    abstract class Document
    {
        public List<Page> Pages = new List<Page>();

        public Document()
        {
            this.CreatePages();
        }
        public abstract void CreatePages();
    }

    class Resume : Document
    {
        public override void CreatePages()
        {
            Pages.Add(new SkillsPage());
            Pages.Add(new EducationPage());
            Pages.Add(new ExperiencePage());
        }
    }

    class Report : Document
    {
        public override void CreatePages()
        {
            Pages.Add(new IntroductionPage());
            Pages.Add(new BibliographyPage());
        }
    }
    #endregion

    #endregion

    #region Abstarct Factory Pattern
    /*
    Source:http://www.dofactory.com/net/abstract-factory-design-pattern

    Definition
        Provide an interface for creating families of related or dependent objects without specifying their concrete classes.

    Participants

    The classes and objects participating in this pattern are:

    AbstractFactory  (ContinentFactory)
        declares an interface for operations that create abstract products
    ConcreteFactory   (AfricaFactory, AmericaFactory)
        implements the operations to create concrete product objects
    AbstractProduct   (Herbivore, Carnivore)
        declares an interface for a type of product object
    Product  (Wildebeest, Lion, Bison, Wolf)
        defines a product object to be created by the corresponding concrete factory
        implements the AbstractProduct interface
    Client  (AnimalWorld)
        uses interfaces declared by AbstractFactory and AbstractProduct classes
    */

    class AbstractFactoryPattern
    {
        public static void Run()
        {
            // Abstract factory #1

            AbstractFactory factory1 = new ConcreteFactory1();
            Client client1 = new Client(factory1);
            client1.Run();

            // Abstract factory #2

            AbstractFactory factory2 = new ConcreteFactory2();
            Client client2 = new Client(factory2);
            client2.Run();

            ContinentFactory factory3 = new AfricaFactory();
            AnimalWorld animal1 = new AnimalWorld(factory3);
            animal1.RunFoodChain();

            ContinentFactory factory4 = new AmericaFactory();
            AnimalWorld animal2 = new AnimalWorld(factory4);
            animal2.RunFoodChain();
        }
    }

    #region Structural Code
    abstract class AbstractFactory
    {
        public abstract AbstractProductA CreateProductA();
        public abstract AbstractProductB CreateProductB();
    }

    class ConcreteFactory1 : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA1();
        }

        public override AbstractProductB CreateProductB()
        {
            return new ProductB1();
        }
    }

    class ConcreteFactory2 : AbstractFactory
    {
        public override AbstractProductA CreateProductA()
        {
            return new ProductA2();
        }

        public override AbstractProductB CreateProductB()
        {
            return new ProductB2();
        }
    }

    abstract class AbstractProductA
    {

    }

    abstract class AbstractProductB
    {
        public abstract void Interact(AbstractProductA product);
    }

    class ProductA1 : AbstractProductA
    {

    }

    class ProductB1 : AbstractProductB
    {
        public override void Interact(AbstractProductA product)
        {
            Console.WriteLine(this.GetType().Name + " interacts with " + product.GetType().Name);
        }
    }

    class ProductA2 : AbstractProductA
    {

    }

    class ProductB2 : AbstractProductB
    {
        public override void Interact(AbstractProductA product)
        {
            Console.WriteLine(this.GetType().Name + " interacts with " + product.GetType().Name);
        }
    }

    class Client

    {
        private AbstractProductA _abstractProductA;
        private AbstractProductB _abstractProductB;

        public Client(AbstractFactory factory)
        {
            _abstractProductB = factory.CreateProductB();
            _abstractProductA = factory.CreateProductA();
        }

        public void Run()
        {
            _abstractProductB.Interact(_abstractProductA);
        }
    }
    #endregion

    #region Real-World Code
    abstract class ContinentFactory
    {
        public abstract Herbivore CreateHerbivore();
        public abstract Carnivore CreateCarnivore();
    }

    class AfricaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Wildebeest();
        }
        public override Carnivore CreateCarnivore()
        {
            return new Lion();
        }
    }

    class AmericaFactory : ContinentFactory
    {
        public override Herbivore CreateHerbivore()
        {
            return new Bison();
        }
        public override Carnivore CreateCarnivore()
        {
            return new Wolf();
        }
    }

    abstract class Herbivore
    {
    }

    abstract class Carnivore
    {
        public abstract void Eat(Herbivore h);
    }

    class Wildebeest : Herbivore
    {
    }

    class Lion : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            // Eat Wildebeest

            Console.WriteLine(this.GetType().Name +
              " eats " + h.GetType().Name);
        }
    }

    class Bison : Herbivore
    {
    }

    class Wolf : Carnivore
    {
        public override void Eat(Herbivore h)
        {
            // Eat Bison

            Console.WriteLine(this.GetType().Name +
              " eats " + h.GetType().Name);
        }
    }

    class AnimalWorld
    {
        private Herbivore _herbivore;
        private Carnivore _carnivore;

        // Constructor

        public AnimalWorld(ContinentFactory factory)
        {
            _carnivore = factory.CreateCarnivore();
            _herbivore = factory.CreateHerbivore();
        }

        public void RunFoodChain()
        {
            _carnivore.Eat(_herbivore);
        }
    }
    #endregion

    #endregion
}
