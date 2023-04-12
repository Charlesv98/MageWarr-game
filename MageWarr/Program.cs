using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace MageWarr
{
    abstract class GameCharacterPrototype
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackPower { get; set; }

        public GameCharacterPrototype(string name, int health, int attackPower)
        {
            Name = name;
            Health = health;
            AttackPower = attackPower;
        }

        public abstract GameCharacterPrototype Clone();
    }

    class Warrior : GameCharacterPrototype
    {
        private static int _nextID = 1;

        public int ID { get; private set; }

        public Warrior(string name, int health, int attackPower) : base(name, health, attackPower)
        {
            ID = _nextID;
            _nextID++;
        }

        public override GameCharacterPrototype Clone()
        {
            return (GameCharacterPrototype)this.MemberwiseClone();
        }
    }

    class Mage : GameCharacterPrototype
    {
        private static int _nextID = 1;

        public int ID { get; private set; }

        public Mage(string name, int health, int attackPower) : base(name, health, attackPower)
        {
            ID = _nextID;
            _nextID++;
        }

        public override GameCharacterPrototype Clone()
        {
            return (GameCharacterPrototype)this.MemberwiseClone();
        }
    }

    class GameCharacterBuilder
    {
        private GameCharacterPrototype _character;

        public GameCharacterBuilder(string characterType)
        {
            switch (characterType)
            {
                case "Warrior":
                    _character = new Warrior("", 0, 0);
                    break;
                case "Mage":
                    _character = new Mage("", 0, 0);
                    break;
            }
        }

        public GameCharacterBuilder WithName(string name)
        {
            _character.Name = name;
            return this;
        }

        public GameCharacterBuilder WithHealth(int health)
        {
            _character.Health = health;
            return this;
        }

        public GameCharacterBuilder WithAttackPower(int attackPower)
        {
            _character.AttackPower = attackPower;
            return this;
        }

        public GameCharacterPrototype Build()
        {
            return _character;
        }
    }

    class Game
    {
        private GameCharacterPrototype _player1;
        private GameCharacterPrototype _player2;

        public void CreatePlayer1(string characterType, string name, int health, int attackPower)
        {
            _player1 = new GameCharacterBuilder(characterType)
                .WithName(name)
                .WithHealth(health)
                .WithAttackPower(attackPower)
                .Build();
        }

        public void CreatePlayer2(string characterType, string name, int health, int attackPower)
        {
            _player2 = new GameCharacterBuilder(characterType)
                .WithName(name)
                .WithHealth(health)
                .WithAttackPower(attackPower)
                .Build();
        }

        public void Play()
        {
            while (_player1.Health > 0 && _player2.Health > 0)
            {
                _player2.Health -= _player1.AttackPower;
                Console.WriteLine($"{_player1.Name} attacked {_player2.Name}. {_player2.Name} has {_player2.Health} health remaining.");

                if (_player2.Health <= 0)
                {
                    Console.WriteLine($"{_player2.Name} has been defeated! {_player1.Name} wins!");
                    return;

                }
                _player1.Health -= _player2.AttackPower;
                Console.WriteLine($"{_player2.Name} attacked {_player1.Name}. {_player1.Name} has {_player1.Health} health remaining.");
                {
                    if (_player1.Health <= 0)
                    {
                        Console.WriteLine($"{_player1.Name} has been defeated! {_player2.Name} wins!");
                        return;
                    }
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.CreatePlayer2("Mage", "Mage Pepa", 200, 25);
            game.CreatePlayer1("Warrior", "Warr Honza", 200, 20);
            game.Play();
        }
    }
}
