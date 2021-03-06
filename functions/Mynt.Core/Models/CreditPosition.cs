﻿using log4net;
using System;
using System.Reflection;

namespace Mynt.Core.Models
{
    public class CreditPosition
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly string symbol;

        private readonly double fee;

        private double ownedQuantity;

        private double btcCredit;

        public CreditPosition(string symbol, double fee, double ownedQuantity, double btcCredit)
        {
            this.symbol = symbol;
            this.fee = fee;
            this.ownedQuantity = ownedQuantity;
            this.btcCredit = btcCredit;
        }

        public string Symbol => symbol;

        public double BtcCredit => btcCredit;

        public double OwnedQuantity => ownedQuantity;

        public void RegisterBuy(double quantity, double rate)
        {
            // Increase invested amount, decrease free amount.
            ownedQuantity += quantity;
            btcCredit -= quantity * rate * (1 + fee);
            log.Info($"Registered a buy for {symbol}. Subtracted {quantity * rate * (1 + fee):#0.##########} from the credit");
        }

        public void RegisterSell(double quantity, double rate)
        {
            // Decrease invested amount, increase free amount.
            ownedQuantity -= quantity;
            btcCredit += quantity * rate * (1 - fee);
            log.Info($"Registered a sell for {symbol}. Added {quantity * rate * (1 - fee):#0.##########} to the credit");
        }
    }
}
