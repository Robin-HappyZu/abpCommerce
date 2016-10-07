using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using HappyZu.CloudStore.Trip.Dto;

namespace HappyZu.CloudStore.Trip
{
    public class ETicketAppService : IETicketAppService
    {
        private readonly ETicketManager _eTicketManager;
        private readonly IUniqueIdManager _uniqueIdManager;
        private readonly MD5 _md5Hash;
        public ETicketAppService(ETicketManager eTicketManager, IUniqueIdManager uniqueIdManager)
        {
            _eTicketManager = eTicketManager;
            _uniqueIdManager = uniqueIdManager;
            _md5Hash = MD5.Create();
        }

        public async Task CreateETicketAsync(int ticketId, int ticketOrderId, int ticketOrderItemId, string description)
        {
            var eTicket = new ETicket();

            eTicket.TicketId = ticketId;
            eTicket.TicketOrderId = ticketOrderId;
            eTicket.TicketorderItemId = ticketOrderItemId;
            eTicket.Description = description;
            eTicket.SerialNo = _uniqueIdManager.CreateId();

            var source = $"{ticketId}|{ticketOrderId}|{ticketOrderItemId}|{eTicket.SerialNo}";

            eTicket.Hash = GetMd5Hash(source);
            eTicket.IsChecked = false;

            await _eTicketManager.InsertETicketAsync(eTicket);
        }

        public async Task<bool> CheckInAsync(int serialNo, string hash, int checkerId)
        {
            var eTicket = await _eTicketManager.GetETicketBySerialNoAndHashAsync(serialNo, hash);
            if (eTicket == null || eTicket.IsChecked) return false;

            eTicket.IsChecked = true;
            eTicket.CheckedOn = DateTime.UtcNow;
            eTicket.CheckerId = checkerId;
            await _eTicketManager.UpdateETicketAsync(eTicket);

            return true;
        }

        public async Task<bool> IsValidAsync(int serialNo, string hash)
        {
            var eTicket = await _eTicketManager.GetETicketBySerialNoAndHashAsync(serialNo, hash);
            return eTicket != null && !eTicket.IsChecked;
        }

        public async Task<IList<ETicketDto>> GetETicketsByTicketOrderIdAsync(int ticketOrderId)
        {
            var eTickets = await _eTicketManager.GetETicketsByTicketOrderIdAsync(ticketOrderId);
            return eTickets.MapTo<List<ETicketDto>>();
        }

        public async Task<ETicketDto> GetETicketByIdAsync(int eTicketId)
        {
            var eTicket = await _eTicketManager.GetETicketByIdAsync(eTicketId);
            return eTicket.MapTo<ETicketDto>();
        }

        private string GetMd5Hash(string source)
        {
            var data = _md5Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

            var builder = new StringBuilder();
            foreach (byte t in data)
            {
                builder.Append(t.ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
