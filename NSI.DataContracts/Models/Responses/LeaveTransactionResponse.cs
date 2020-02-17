
namespace NSI.DataContracts.Models.Responses
{
    public class LeaveTransactionResponse
    {
        /// <summary>
        /// Unique identifier for Leave Transaction
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// How much days should be taken off the Leave balance
        /// </summary>
        public long Days { get; set; }
        /// <summary>
        /// Who approved this transaction
        /// </summary>
        public long ApprovedByID { get; set; }
        /// <summary>
        /// Which leave balance does this transaction refer to
        /// </summary>
        public long LeaveBalanceId { get; set; }
        
    }
}