using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Owin;
using Webserver.Options;

namespace Webserver.Middleware
{
    public class GitReceivePackMiddleware : OwinMiddleware
    {
        private readonly WebserverOptions _options;

        public GitReceivePackMiddleware(OwinMiddleware next, WebserverOptions options)
            : base(next)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            _options = options;
        }

        public override Task Invoke(IOwinContext context)
        {
            Trace.WriteLine("Invoke GetReceivePackMiddleware");

            var service = context.Request.Query["service"] ?? String.Empty;
            if (context.Request.Path.Value.EndsWith("/info/refs") && service.Equals("git-receive-pack"))
            {
                context.Response.Headers.Add("Expires", new[] { DateTime.Now.ToString("R") });
                context.Response.Headers.Add("Pragma", new[] { "no-cache" });
                context.Response.Headers.Add("Cache-Control", new[] { "no-cache, max-age=0, must-revalidate" });
                context.Response.Headers.Add("Content-Type", new[] { "application/x-git-receive-pack-advertisement" });

                //var process = Process.Start("git", string.Format("receive-pack --stateless-rpc --advertise-refs {0}repo", _options.BasePath));
                //process.WaitForExit(1000);
                //var output = process.StandardOutput;

                //var body = output.ReadToEnd();

                //var packet = "# service=git-receive-pack\n";
                //var length = packet.Length + 4;
                //var hex = "0123456789abcdef";
                //var prefix = hex[hex.Length - 1 >> 12] & 0xf;
                //prefix = prefix + hex[length - 1 >> 8] & 0xf;
                //prefix = prefix + hex[length - 1 >> 4] & 0xf;
                //prefix = prefix + hex[length - 1] & 0xf;
                //var body = string.Format("#{0}#{1}}}0000", prefix, packet);


                var body = "00900000000000000000000000000000000000000000 capabilities^{}  report-status delete-refs side-band-64k quiet ofs-delta agent=git/1.8.0.msysgit.00000";
                context.Response.Write(body);

                return Globals.CompletedTask;
            }

            return Next.Invoke(context);
        }

        void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /*
         * 
        
  res.setHeader 'Expires', 'Fri, 01 Jan 1980 00:00:00 GMT'
  res.setHeader 'Pragma', 'no-cache'
  res.setHeader 'Cache-Control', 'no-cache, max-age=0, must-revalidate'
  res.setHeader 'Content-Type', 'application/x-git-receive-pack-advertisement'

  packet = "# service=git-receive-pack\n"
  length = packet.length + 4
  hex = "0123456789abcdef"
  prefix = hex.charAt (length >> 12) & 0xf
  prefix = prefix + hex.charAt (length >> 8) & 0xf
  prefix = prefix + hex.charAt (length >> 4) & 0xf
  prefix = prefix + hex.charAt length & 0xf
  res.write "#{prefix}#{packet}0000"

         * 
         */
    }
}
