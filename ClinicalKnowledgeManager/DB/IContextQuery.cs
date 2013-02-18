// -----------------------------------------------------------------------
// <copyright file="IContextQuery.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ClinicalKnowledgeManager.DB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Defines the parameters that should be implemented by any context-based query
    /// </summary>
    public interface IContextQuery
    {
        string InformationRecipient { get; set; }
        string SearchCode { get; set; }
        string SearchCodeSystem { get; set; }
    }
}
