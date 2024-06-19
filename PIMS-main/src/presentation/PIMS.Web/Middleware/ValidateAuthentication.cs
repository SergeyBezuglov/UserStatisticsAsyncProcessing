namespace PIMS.Web.Middleware
{
    /// <summary>
    /// Подтвердить аутентификацию.
    /// </summary>
    public class ValidateAuthentication : IMiddleware
    {

        /// <summary>
        /// Вызывает <see cref="Task"/> asynchronously.
        /// </summary>
        /// <param name="context">Контекст.</param>
        /// <param name="next">Следующий.</param>
        /// <returns>Возвращение значения задачи (Task).</returns>
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var oldPath = context.Request.Path;
            var response = context.Response;

            //if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            //{
            //    context.Request.Path = $"/api/v1/ADAuthentication/Login";
            //}
              

            await next(context);
            //if (context.User.Identity!.IsAuthenticated)
            //    await next(context);
            //else
            //    await context.ChallengeAsync();
            
        }
    }
}
