using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Domain.Specifications.TokenSpecification
{
    public class FindTokenByUserIdAndTokenSpecification : Specification<Entities.Token>
    {
        public FindTokenByUserIdAndTokenSpecification(long userId, string refreshToken)
        {
            Criteria = entiy => entiy.UserId == userId && entiy.RefreshToken == refreshToken;
        }
    }
}
