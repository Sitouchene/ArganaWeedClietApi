using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArganaWeedClietApi
{
    public class ClientRequestResponse<T, E> where T : BaseRequest where E : BaseResponse
    {
        private readonly T request;

        private readonly string target;

        public ClientRequestResponse(string target)
        {
            this.target = target;

        }

        public ClientRequestResponse(T request, string target)
        {
            this.request = request;
            this.target = target;
        }

        public async Task<E> ReceivePost()
        {
            E res = null;

            try
            {
                string raw = ServiceSerializer<T>.Serialize(request);

                HttpService service = new HttpService();

                string result = await service.PostAsync(target, raw);

                res = ServiceSerializer<E>.Deserialize(result);
            }
            catch (Exception ex)
            {
                RemoteCallException remoteCallException = new RemoteCallException(
                    $"Error while Calling server {target}", ex);

                throw remoteCallException;
            }

            return res;
        } 
        public async Task<E> ReceiveGet()
        {
            E res = null;

            try
            {
                HttpService service = new HttpService();

                string result = await service.GetAsync(target);

                res = ServiceSerializer<E>.Deserialize(result);
            }
            catch (Exception ex)
            {
                RemoteCallException remoteCallException = new RemoteCallException(
                    $"Error while Calling server {target}", ex);

                throw remoteCallException;
            }

            return res;
        }
    }
}
