using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyZu.CloudStore.Common.Dto
{
    public class ResultOutputDto
    {
        public static readonly ResultOutputDto Successed = new ResultOutputDto(true, 0, null,null);
        public static readonly ResultOutputDto Failed = new ResultOutputDto(false, 0, null, null);

        public ResultOutputDto(bool status, int code, string message, int? entityId)
        {
            Status = status;
            Code = code;
            Message = message;
            EntityId = entityId;
        }
        /// <summary>
        /// 状态
        /// </summary>
        public bool Status { get; private set; }

        public int? EntityId { get; private set; }
        /// <summary>
        /// 调用信息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 返回码
        /// </summary>
        public int Code { get; private set; }

        public static ResultOutputDto Success()
        {
            return Successed;
        }

        public static ResultOutputDto Success(int entityId)
        {
            return new ResultOutputDto(true, 0, null,entityId);
        }

        public static ResultOutputDto Fail()
        {
            return Failed;
        }

        public static ResultOutputDto FromResult(bool result)
        {
            return result ? Successed : Failed;
        }

        public static ResultOutputDto Fail(int errCode, string message = null)
        {
            return new ResultOutputDto(false, errCode, message, null);
        }

        public static ResultOutputDto Exception(Exception ex)
        {
            return new ResultOutputDto(false, 500, ex.Message, null);
        }
    }
}
