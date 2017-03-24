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
        [RoutePrefix("api/students")]
        public class StudentsAPIController : ApiController
        {
            [Route]
            public HttpResponseMessage Get()
            {

                StudentService srv = new StudentService();
                List<Student> data = srv.GetAll().ToList();
                ItemsResponse<Student> res = new ItemsResponse<Student>();
                res.Items = data;
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            [Route("{id:int}")]
            public HttpResponseMessage Get(int id)

            {
                try
                {
                    StudentService srv = new StudentService();
                    Student data = srv.Get(id);
                ItemResponse<Student> res = new ItemResponse<Student>();
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
            public HttpResponseMessage Post(Student data)
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
                    StudentService srv = new StudentService();
                    int studentId = srv.Create(data);
                    ItemResponse<int> res = new ItemResponse<int>();
                    res.Item = studentId;
                    return Request.CreateResponse(HttpStatusCode.OK, res);
                }
            }
            [Route("{id:int}")]
            public HttpResponseMessage Delete(int id)
            {
                StudentService srv = new StudentService();
                srv.Delete(id);
                SuccessResponse res = new SuccessResponse();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            [Route("{id:int}")]//parameters {id:in} prevent named route
            public HttpResponseMessage Put(Student data, int id)//int id catches the StudentID


            {
                StudentService srv = new StudentService();
                try
                {

                    Student tempData = srv.Get(id);

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
