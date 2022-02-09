using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeppelinWebsite.Models;
using ZeppelinWebsite.ViewModel;

namespace ZeppelinWebsite.Controllers
{
    public class RatingController : Controller
    {

        private ZeppelinnDBEntities objzeppelinDBEntities;

        public RatingController()

        {
            
            objzeppelinDBEntities = new ZeppelinnDBEntities();

        }


        // GET: Rating
        public ActionResult Index()
        {

            IEnumerable<ArticleViewModel> listOfArticleViewModels = (from objArticle in objzeppelinDBEntities.Articles
                                                                     select new ArticleViewModel()
                                                                     {
                                                                         ArticleDescription=objArticle.ArticleDescription,
                                                                         ArticleId=objArticle.ArticleId,
                                                                         ArticleName=objArticle.ArticleName

                                                                     }).ToList();
            return View(listOfArticleViewModels);
        }



        public ActionResult ShowComment(int articleId)
        {

            IEnumerable<CommentViewModel> listOfCommentViewModels = (from objComment in objzeppelinDBEntities.Comments
                                                                     where objComment.ArticleId == articleId
                                                                     select new CommentViewModel()
                                                                     {

                                                                         ArticleId = objComment.ArticleId,
                                                                         CommentDescription=objComment.CommentDescription,
                                                                         CommentId=objComment.CommentId,
                                                                         CommentedOn=objComment.CommentedOn,
                                                                         Rating=objComment.Rating



                                                                     }).Where(X => X.Rating==3).ToList();

            ViewBag.ArticleId = articleId;
            return View(listOfCommentViewModels);
        }



        [HttpPost]
        public ActionResult AddComment(int articleId, int rating, string articleComment)
        {
            Comment objComment = new Comment();
            objComment.ArticleId = articleId;
           
            objComment.CommentDescription = articleComment;
            objComment.CommentedOn = DateTime.Now;
            objComment.Rating = rating;
            objComment.GuestId = Guid.NewGuid();

            objzeppelinDBEntities.Comments.Add(objComment);
            objzeppelinDBEntities.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}