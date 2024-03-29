﻿
using Sarafi.Domain.Common;
using Sarafi.Domain.Enums;

namespace Sarafi.Domain.Entities; 
public class Connection : AggregateRoot, IMultiTenant
{
    public long FromUserId { private set; get; }
    public virtual User FromUser { get; private set; }
    public long ToUserId { private set; get; }
    public virtual User ToUser { get; private set; }
    public ConnectionStatus ConnectionStatus { get; private set; }
    public long CompanyId { get; set; }

    public void SetCompanyId(long companyId) => CompanyId = companyId;
}
