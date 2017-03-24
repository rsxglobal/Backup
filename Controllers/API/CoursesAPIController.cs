using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using StudentLab.Domain;
using StudentLab.Models.Response;
using StudentLab.Services;
using System.Web.Http.ModelBinding;

namespace StudentLab.Controllers.API
{
    [RoutePrefix("api/courses")]
    public class CoursesAPIController : ApiController
    {
        [Route]
        public HttpResponseMessage Get()
        {

            CourseService srv = new CourseService();
            List<Course> data = srv.GetAll().ToList();
            ItemsResponse<Course> res = new ItemsResponse<Course>();
            res.Items = data;
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        [Route("{id:int}")]
        public HttpResponseMessage Get(int id)

        {
            try
            {
                CourseService srv = new CourseService();
                Course data = srv.Get(id);
                ItemResponse<Course> res = new ItemResponse<Course>();
                res.Item = data;
                return Request.CreateResponse(HttpStatusCode.OK, res);

            }
            catch (Exception ex)
            {
                ErrorResponse res = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, res);
            }
        }
        [Route]
        public HttpResponseMessage Post(Course data)
        {
            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (ModelState item in ModelState.Values)
                {
                    foreach (ModelError err in item.Errors)
                    {
                        if (string.IsNullOrWhiteSpace(err.ErrorMessage) && err.Exception != null)
                        {
                            errors.Add(err.Exception.Message);
                        }
                        else
                        {
                            errors.Add(err.ErrorMessage);
                        }

                    }

                }
                ErrorResponse res = new ErrorResponse(errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
            }
            else
            {
                CourseService srv = new CourseService();
                int CourseId = srv.Create(data);
                ItemResponse<int> res = new ItemResponse<int>();
                res.Item = CourseId;
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
        }
        [Route("{id:int}")]
        public HttpResponseMessage Delete(int id)
        {
            CourseService srv = new CourseService();
            srv.Delete(id);
            SuccessResponse res = new SuccessResponse();
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }
        [Route("{id:int}")]//parameters {id:in} prevent named route
        public HttpResponseMessage Put(Course data, int id)//int id catches the CourseID


        {
            CourseService srv = new CourseService();
            try
            {

                Course tempData = srv.Get(id);

            }
            catch (Exception ex)
            {
                ErrorResponse res = new ErrorResponse(ex.Message);
                return Request.CreateResponse(HttpStatusCode.NotFound, res);
            }

            if (!ModelState.IsValid)
            {
                List<string> errors = new List<string>();
                foreach (ModelState item in ModelState.Values)
                {
                    foreach (ModelError err in item.Errors)
                    {
                        if (string.IsNullOrWhiteSpace(err.ErrorMessage) && err.Exception != null)
                        {
                            errors.Add(err.Exception.Message);
                        }
                        else
                        {
                            errors.Add(err.ErrorMessage);
                        }

                    }

                }
                ErrorResponse res = new ErrorResponse(errors);
                return Request.CreateResponse(HttpStatusCode.BadRequest, res);
            }
            else
            {

                srv.Update(data);
                SuccessResponse res = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
        }
    }
}
