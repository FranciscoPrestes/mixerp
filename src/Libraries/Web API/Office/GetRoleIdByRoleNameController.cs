// ReSharper disable All
using MixERP.Net.Framework;
using MixERP.Net.Entities.Office;
using MixERP.Net.Schemas.Office.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MixERP.Net.ApplicationState.Cache;
using MixERP.Net.Common.Extensions;
using PetaPoco;
using MixERP.Net.EntityParser;
namespace MixERP.Net.Api.Office
{
    /// <summary>
    /// Provides a direct HTTP access to execute the function GetRoleIdByRoleName.
    /// </summary>
    [RoutePrefix("api/v1.5/office/procedures/get-role-id-by-role-name")]
    public class GetRoleIdByRoleNameController : ApiController
    {
        /// <summary>
        /// Login id of application user accessing this API.
        /// </summary>
        public long _LoginId { get; set; }

        /// <summary>
        /// User id of application user accessing this API.
        /// </summary>
        public int _UserId { get; set; }

        /// <summary>
        /// Currently logged in office id of application user accessing this API.
        /// </summary>
        public int _OfficeId { get; set; }

        /// <summary>
        /// The name of the database where queries are being executed on.
        /// </summary>
        public string _Catalog { get; set; }

        /// <summary>
        ///     The GetRoleIdByRoleName repository.
        /// </summary>
        private readonly IGetRoleIdByRoleNameRepository repository;

        public class Annotation
        {
            public string RoleName { get; set; }
        }


        public GetRoleIdByRoleNameController()
        {
            this._LoginId = AppUsers.GetCurrent().View.LoginId.ToLong();
            this._UserId = AppUsers.GetCurrent().View.UserId.ToInt();
            this._OfficeId = AppUsers.GetCurrent().View.OfficeId.ToInt();
            this._Catalog = AppUsers.GetCurrentUserDB();

            this.repository = new GetRoleIdByRoleNameProcedure
            {
                _Catalog = this._Catalog,
                _LoginId = this._LoginId,
                _UserId = this._UserId
            };
        }

        public GetRoleIdByRoleNameController(IGetRoleIdByRoleNameRepository repository, string catalog, LoginView view)
        {
            this._LoginId = view.LoginId.ToLong();
            this._UserId = view.UserId.ToInt();
            this._OfficeId = view.OfficeId.ToInt();
            this._Catalog = catalog;

            this.repository = repository;
        }

        /// <summary>
        ///     Creates meta information of "get role id by role name" annotation.
        /// </summary>
        /// <returns>Returns the "get role id by role name" annotation meta information to perform CRUD operation.</returns>
        [AcceptVerbs("GET", "HEAD")]
        [Route("annotation")]
        [Route("~/api/office/procedures/get-role-id-by-role-name/annotation")]
        public EntityView GetAnnotation()
        {
            if (this._LoginId == 0)
            {
                return new EntityView();
            }
            return new EntityView
            {
                Columns = new List<EntityColumn>()
                                {
                                        new EntityColumn { ColumnName = "_role_name",  PropertyName = "RoleName",  DataType = "string",  DbDataType = "text",  IsNullable = false,  IsPrimaryKey = false,  IsSerial = false,  Value = "",  MaxLength = 0 }
                                }
            };
        }




        [AcceptVerbs("POST")]
        [Route("execute")]
        [Route("~/api/office/procedures/get-role-id-by-role-name/execute")]
        public int Execute([FromBody] Annotation annotation)
        {
            try
            {
                this.repository.RoleName = annotation.RoleName;


                return this.repository.Execute();
            }
            catch (UnauthorizedException)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
            }
            catch (MixERPException ex)
            {
                throw new HttpResponseException(new HttpResponseMessage
                {
                    Content = new StringContent(ex.Message),
                    StatusCode = HttpStatusCode.InternalServerError
                });
            }
            catch
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
        }
    }
}