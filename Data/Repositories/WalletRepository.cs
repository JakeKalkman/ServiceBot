using Common.Enums;
using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ServiceBotContext _db;
        public WalletRepository(ServiceBotContext db)
        {
            _db = db;
        }

        public async Task<bool> AddToWallet(string walletUserId, double amount, AmountType type, string senderUserId, string commandUsed)
        {
            var wallet = await _db.Wallet.FirstOrDefaultAsync(x => x.WalletUserId == walletUserId);
            

            if(wallet == null)
            {
                wallet = new Wallet() { WalletUserId = walletUserId };
            }

            var walletTransaction = new WalletTransaction
            {
                SendersWalletId = 1,
                RecieversWalletId = wallet.Id,
                CommandUsed = commandUsed,
                CommandUser = senderUserId,
                Amount = amount
            };

            switch (type)
            {
                case AmountType.FlatGp:
                    walletTransaction.Type = AmountType.FlatGp;
                    walletTransaction.OldValue = wallet.WalletGpAmount;
                    wallet.WalletGpAmount += amount;
                    walletTransaction.NewValue = wallet.WalletGpAmount;                 
                    break;
                case AmountType.USD:
                    walletTransaction.Type = AmountType.USD;
                    walletTransaction.OldValue = wallet.WalletUsdAmount;
                    wallet.WalletUsdAmount += amount;
                    walletTransaction.NewValue = wallet.WalletUsdAmount;
                    break;
                default:
                    break;
            }

            _db.Add(walletTransaction);
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SendToWalletFromWallet(string walletUserId, string receiverWalletUserId, double amount, AmountType type, string commandUsed)
        {
            var wallet = await _db.Wallet.FirstOrDefaultAsync(x => x.WalletUserId == walletUserId);
            var receivingWallet = await _db.Wallet.FirstOrDefaultAsync(x => x.WalletUserId == receiverWalletUserId);


            if (wallet == null)
            {
                wallet = new Wallet() { WalletUserId = walletUserId };
            }
            if(receivingWallet == null)
            {
                receivingWallet = new Wallet() { WalletUserId = receiverWalletUserId };
            }

            var walletTransaction = new WalletTransaction
            {
                SendersWalletId = wallet.Id,
                RecieversWalletId = receivingWallet.Id,
                CommandUsed = commandUsed,
                CommandUser = walletUserId,
                Amount = Math.Round(amount * -1, 2)
            };

            var receivingWalletTransaction = new WalletTransaction
            {
                SendersWalletId = wallet.Id,
                RecieversWalletId = receivingWallet.Id,
                CommandUsed = commandUsed,
                CommandUser = walletUserId,
                Amount = amount
            };

            switch (type)
            {
                case AmountType.FlatGp:
                    if (wallet.WalletGpAmount < amount) { return false; }

                    walletTransaction.Type = AmountType.FlatGp;
                    receivingWalletTransaction.Type = AmountType.FlatGp;
                    walletTransaction.OldValue = wallet.WalletGpAmount;
                    receivingWalletTransaction.OldValue = receivingWallet.WalletGpAmount;
                    wallet.WalletGpAmount -= amount;
                    receivingWallet.WalletGpAmount += amount;
                    walletTransaction.NewValue = wallet.WalletGpAmount;
                    receivingWalletTransaction.NewValue = receivingWallet.WalletGpAmount;
                    break;
                case AmountType.USD:
                    if (wallet.WalletUsdAmount < amount) { return false; }

                    walletTransaction.Type = AmountType.USD;
                    receivingWalletTransaction.Type = AmountType.USD;
                    walletTransaction.OldValue = wallet.WalletUsdAmount;
                    receivingWalletTransaction.OldValue = receivingWallet.WalletUsdAmount;
                    wallet.WalletUsdAmount -= amount;
                    receivingWallet.WalletUsdAmount += amount;
                    walletTransaction.NewValue = wallet.WalletUsdAmount;
                    receivingWalletTransaction.NewValue = receivingWallet.WalletUsdAmount;
                    break;
                default:
                    break;
            }

            _db.Add(walletTransaction);
            _db.Add(receivingWalletTransaction);


            await _db.SaveChangesAsync();

            return true;
        }
    }
}
