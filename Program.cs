using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USCoinJar
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public interface ICoinJar
    {
        void Accept(ICoin coin);

        IVolume TotalVolume { get; }

        ICurrency ActualAmount { get; }

        IVolume ActualVolume { get; }

        void Reset();
    }

    public interface ICoin
    {
        IVolume Volume { get; }

        ICurrency Value { get; }
    }

    public interface IVolume
    {
        long Unit { get; set; }

        double InRelativeMeasure();
    }

    public interface ICurrency
    {
        long UnitPrice { get; set; }

        double InCurrency();
    }

    /// <summary>
    /// For this class, One unit price is equal to One cent.
    /// </summary>
    public class USCurrency : ICurrency
    {
        public long UnitPrice { get; set; }

        public double InCurrency()
        {
            return UnitPrice / 100;
        }
    }

    public class FluidOunces : IVolume
    {
        public long Unit
        {
            get;
            set;
        }

        /// <summary>
        /// Considering Unit is 10000 times smaller than one Fluid Ounce.
        /// </summary>
        /// <returns></returns>
        public double InRelativeMeasure()
        {
            return (double)this.Unit / 10000;
        }
    }

    public abstract class UsCoin : ICoin
    {
        public string Owner
        {
            get
            {
                return "Federal Reserve";
            }
        }

        public abstract IVolume Volume { get; }

        public abstract ICurrency Value { get; }
    }

    public class Penny : UsCoin
    {
        private IVolume volume;
        private ICurrency currency;

        public Penny()
        {
            volume = new FluidOunces();
            volume.Unit = 122;
            currency = new USCurrency();
            currency.UnitPrice = 1;
        }

        public override IVolume Volume
        {
            get
            {
                return volume;
            }
        }

        public override ICurrency Value
        {
            get
            {
                return currency;
            }
        }
    }

    public class Nickel : UsCoin
    {
        private IVolume volume;
        private ICurrency currency;

        public Nickel()
        {
            volume = new FluidOunces();
            volume.Unit = 243;
            currency = new USCurrency();
            currency.UnitPrice = 5;
        }

        public override IVolume Volume
        {
            get
            {
                return volume;
            }
        }

        public override ICurrency Value
        {
            get
            {
                return currency;
            }
        }
    }

    public class Dime : UsCoin
    {
        private IVolume volume;
        private ICurrency currency;

        public Dime()
        {
            volume = new FluidOunces();
            volume.Unit = 115;
            currency = new USCurrency();
            currency.UnitPrice = 10;
        }

        public override IVolume Volume
        {
            get
            {
                return volume;
            }
        }

        public override ICurrency Value
        {
            get
            {
                return currency;
            }
        }
    }

    public class Quarter : UsCoin
    {
        private IVolume volume;
        private ICurrency currency;

        public Quarter()
        {
            volume = new FluidOunces();
            volume.Unit = 270;
            currency = new USCurrency();
            currency.UnitPrice = 25;
        }

        public override IVolume Volume
        {
            get
            {
                return volume;
            }
        }

        public override ICurrency Value
        {
            get
            {
                return currency;
            }
        }
    }

    public class HalfDollar : UsCoin
    {
        private IVolume volume;
        private ICurrency currency;

        public HalfDollar()
        {
            volume = new FluidOunces();
            volume.Unit = 534;
            currency = new USCurrency();
            currency.UnitPrice = 50;
        }

        public override IVolume Volume
        {
            get
            {
                return volume;
            }
        }

        public override ICurrency Value
        {
            get
            {
                return currency;
            }
        }
    }

    public class Dollar : UsCoin
    {
        private IVolume volume;
        private ICurrency currency;

        public Dollar()
        {
            volume = new FluidOunces();
            volume.Unit = 800;                  // just a guess, could not get the real figures....
            currency = new USCurrency();
            currency.UnitPrice = 100;
        }

        public override IVolume Volume
        {
            get
            {
                return volume;
            }
        }

        public override ICurrency Value
        {
            get
            {
                return currency;
            }
        }
    }

    public class MyCoinJar : ICoinJar
    {
        private List<ICoin> coinHeap;
        private FluidOunces totalVolume;
        private ICurrency actualAmount;
        private FluidOunces actualVolume;

        public IVolume TotalVolume
        {
            get
            {
                return totalVolume;
            }
        }

        public ICurrency ActualAmount
        {
            get
            {
                return actualAmount;
            }
        }

        public IVolume ActualVolume
        {
            get
            {
                return actualVolume;
            }
        }

        public MyCoinJar()
        {
            totalVolume = new FluidOunces();
            totalVolume.Unit = 320000;
            Reset();
        }

        public void Accept(ICoin coin)
        {
            if (coin.GetType().BaseType != typeof(UsCoin))
                throw new InValidCoinException("MyCoinJar accepts only UsCoin");
            if (this.TotalVolume.Unit < (this.actualVolume.Unit + coin.Volume.Unit))
                throw new CoinOverFlowException();

            coinHeap.Add(coin);
            actualVolume.Unit += coin.Volume.Unit;
            actualAmount.UnitPrice += coin.Value.UnitPrice;
        }

        public void Reset()
        {
            coinHeap = new List<ICoin>();
            actualVolume = new FluidOunces();
            actualAmount = new USCurrency();
        }
    }

    public class CoinOverFlowException : Exception
    {
        public CoinOverFlowException()
            : base("Coins overflowed the jar")
        {
        }
    }

    public class InValidCoinException : Exception
    {
        public InValidCoinException(string message)
            : base(message)
        {
        }
    }
}
