using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Mre.Sb.UnidadAdministrativa.HttpApi
{
    [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial interface IVentanillaClient
    {
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PagedResultDto<VentanillaDto>> ObtenerPorUnidadAdministrativaIdAsync(System.Guid unidadAdministrativaId);

        /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>Success</returns>
        /// <exception cref="ApiException">A server side error occurred.</exception>
        System.Threading.Tasks.Task<PagedResultDto<VentanillaDto>> ObtenerPorUnidadAdministrativaIdAsync(System.Guid unidadAdministrativaId, System.Threading.CancellationToken cancellationToken);

        void SetAccessToken(string accessToken);

        void AddHeaders(string name, string value);

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial class VentanillaDto
    {

        [System.Text.Json.Serialization.JsonPropertyName("id")]
        public System.Guid Id { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("administrativeUnitId")]
        public System.Guid AdministrativeUnitId { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("name")]
        public string Name { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("isPresentialAttention")]
        public bool IsPresentialAttention { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("isVirtualAttention")]
        public bool IsVirtualAttention { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("attentionStart")]
        public string AttentionStart { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("attentionEnd")]
        public string AttentionEnd { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial class RemoteServiceErrorResponse
    {

        [System.Text.Json.Serialization.JsonPropertyName("error")]
        public RemoteServiceErrorInfo Error { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial class RemoteServiceErrorInfo
    {

        [System.Text.Json.Serialization.JsonPropertyName("code")]
        public string Code { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string Message { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("details")]
        public string Details { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("data")]
        public System.Collections.Generic.IDictionary<string, object> Data { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("validationErrors")]
        public System.Collections.Generic.ICollection<RemoteServiceValidationErrorInfo> ValidationErrors { get; set; }

    }

    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "13.15.10.0 (NJsonSchema v10.6.10.0 (Newtonsoft.Json v12.0.0.0))")]
    public partial class RemoteServiceValidationErrorInfo
    {

        [System.Text.Json.Serialization.JsonPropertyName("message")]
        public string Message { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("members")]
        public System.Collections.Generic.ICollection<string> Members { get; set; }

    }

}
