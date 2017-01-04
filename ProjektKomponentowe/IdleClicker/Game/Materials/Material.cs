using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace IdleClicker
{
    public delegate void BoostMaterialHandler(Material m);

    /// <summary>
    /// Klasa opisująca surowiec.
    /// </summary>
    public class Material : IRequired, IReducible, IProductable
    {
        public event BoostMaterialHandler onChangeMaterial;

        /// <summary>
        /// Nazwa surowca.
        /// </summary>
        String key;

        /// <summary>
        /// Ścieżka do ikona jako graficznej reprezentacji surowca.
        /// </summary>
        ImageSource iconSource;

        /// <summary>
        /// Określa czy materiał został dodany do zegara gry (Aby go zwiększać)
        /// </summary>
        bool addedToGameTick;

        /// <summary>
        /// Obecna ilość surowca.
        /// </summary>
        double currentAmount = 0;
        /// <summary>
        /// Obecny przyrost zasobu na tick zegara.
        /// </summary>
        double currentIncreaseQuantity = 0;
        /// <summary>
        /// Początkowy przyrost zasobu na tick zegara.
        /// </summary>
        double beginningIncreaseQuantity = 0;

        public Dictionary<Building, double> buildingBonuses = new Dictionary<Building, double>();

        public Dictionary<Building, double> BuildingBonuses { get { return BuildingBonuses; } }
        

        public void AddProductiveBuilding(Building building, double increaseQuantity)
        {
            BuildingBonuses.Add(building, increaseQuantity);

            AddBonusQuantity(increaseQuantity, 0);
        }

        /// <summary>
        /// Właściwość, która umożliwia pobranie lub ustawienie nazwy surowca.
        /// </summary>
        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }
        /// <summary>
        /// Właściwość, która umożliwia pobranie icony danego surowca; 
        /// </summary>
        public ImageSource IconSource
        {
            get
            {
                return iconSource;
            }
            set
            {
                iconSource = value;
            }
        }

        /// <summary>
        /// Właściwość, która umożliwia ustawienie, bądź pobranie obecnej ilości danego surowca.
        /// </summary>
        public double CurrentAmount
        {
            get
            {
                return currentAmount;
            }
            set
            {
                currentAmount = value;
            }
        }

        /// <summary>
        /// Właściwość, która umożliwia ustawienie, bądź pobranie początkowego przyrostu danego surowca na tick zegara.
        /// </summary>
        public double BeginningIncreaseQuantity
        {
            get
            {
                return beginningIncreaseQuantity;
            }
            set
            {
                beginningIncreaseQuantity = value;
            }
        }

        /// <summary>
        /// Właściwość, która umożliwia ustawienie, bądź pobranie obecnego przyrostu danego surowca na tick zegara.
        /// </summary>
        public double CurrentIncreaseQuantity
        {
            get
            {
                return currentIncreaseQuantity;
            }
            set
            {
                currentIncreaseQuantity = value;
                if (currentIncreaseQuantity == 0)
                {
                    if (addedToGameTick)
                    {
                        addedToGameTick = false;
                        GameEngine.GameTimer.OnTick -= GameTimer_OnTick;
                    }
                }
                else
                    if (!addedToGameTick)
                    {
                        addedToGameTick = true;
                        GameEngine.GameTimer.OnTick += GameTimer_OnTick;
                    }             
            }
        }

        private void GameTimer_OnTick(TickEventArgs e)
        {
            BoostMaterial();
        }

        public RequireType RequireType
        {
            get
            {
                return RequireType.Material;
            }
        }

        /// <summary>
        /// Metoda, która umożliwia zwiększenie przyrostu zasobu na tick zegara, wyrażonego w procentach.
        /// </summary>
        /// <param name="percentage">Parametr, określający bonus wyrażony w procentach.</param>
        /// <param name="time">Paramter, określający czas trwania aktywności bonusa.</param>
        public void AddBonusPercentage(double percentage, int time)
        {
            CurrentIncreaseQuantity += percentage/100 * beginningIncreaseQuantity;

            TickAction action = new TickAction(time);
            action.Actions += () => { CurrentIncreaseQuantity -= percentage * beginningIncreaseQuantity; };

            GameEngine.ActionList.AddAction(action);

        }

        public void AddIncreaseCount(double value)
        {
            AddBonusQuantity(value, 0);
        }

        /// <summary>
        /// Metoda, która umożliwia zwiększanie przyrostu zasobu na tick zegara, wyrażonego liczbą.
        /// </summary>
        /// <param name="quantity">Paramter, określający bonus wyrażone w liczbie jednostek danego zasobu.</param>
        /// <param name="time">Paramter, określający czas trwania aktywności bonusa.</param>
        public void AddBonusQuantity(double quantity, int time)
        {
            if (time == 0)
            {
                CurrentIncreaseQuantity += quantity;
                return;
            }

            CurrentIncreaseQuantity += quantity;

            TickAction action = new TickAction(time);
            action.Actions += () => { CurrentIncreaseQuantity -= quantity;  };

            GameEngine.ActionList.AddAction(action);
        }

        /// <summary>
        /// Metoda, odpowiadająca za zwiększenie ilości surowca o ustawiony przyrost zasobu na tick zegara.
        /// </summary>
        public void BoostMaterial()
        {
            currentAmount += CurrentIncreaseQuantity;
            onChangeMaterial(this);
        }

        /// <summary>
        /// Metoda, odpowiadająca za zredukowanie ilości surowca o podaną liczbę jednostek.
        /// </summary>
        /// <param name="value">Paramter, określający liczbę jednostek surowca, o którą ogólna ilość ma zostać zredukowana.</param>
        public void ReduceMaterial(double value)
        {
            currentAmount -= value;
            onChangeMaterial(this);
        }

        public ImageSource GetIcon()
        {
            return IconSource;
        }

        public double GetValue()
        {
            return currentAmount;
        }
    }
}
