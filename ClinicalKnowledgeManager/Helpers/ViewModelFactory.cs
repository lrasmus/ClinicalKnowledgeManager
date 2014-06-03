using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using ClinicalKnowledgeManager.ViewModels;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.Helpers
{
    public class ViewModelFactory
    {
        private TopicRepository Repository { get; set; }

        public ViewModelFactory(TopicRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<SubTopicDetail> BuildSubTopicsForTopic(Topic topic, List<SubTopic> relevantSubTopics)
        {
            IEnumerable<SubTopic> subTopics = Repository.GetSubTopicsForItem("Topic", topic.Id);
            if (relevantSubTopics != null && relevantSubTopics.Count > 0)
            {
                subTopics = subTopics.Where(x => relevantSubTopics.FirstOrDefault(y => y.Id == x.Id) != null);
            }
            IEnumerable<SubTopicDetail> subTopicDetails = subTopics.Select(x => new SubTopicDetail()
            {
                Level = 2,
                SubTopic = x,
                Contents = Repository.GetContentsForItem("SubTopic", x.Id)
            }).ToList();
            foreach (var child in subTopicDetails)
            {
                RecursivelyBuildSubTopicsForSubTopics(child, 3, relevantSubTopics);
            }

            return subTopicDetails;
        }

        private void RecursivelyBuildSubTopicsForSubTopics(SubTopicDetail subTopic, int level, List<SubTopic> relevantSubTopics)
        {
            IEnumerable<SubTopic> subTopics = Repository.GetSubTopicsForItem("SubTopic", subTopic.SubTopic.Id);
            if (relevantSubTopics != null && relevantSubTopics.Count > 0)
            {
                subTopics = subTopics.Where(x => relevantSubTopics.FirstOrDefault(y => y.Id == x.Id) != null);
            }
            subTopic.SubTopics = subTopics.Select(x => new SubTopicDetail()
            {
                Level = level,
                SubTopic = x,
                Contents = Repository.GetContentsForItem("SubTopic", x.Id)
            }).ToList();

            foreach (var child in subTopic.SubTopics)
            {
                RecursivelyBuildSubTopicsForSubTopics(child, level + 1, relevantSubTopics);
            }
        }

        public SubTopicDetail BuildSubTopicDetail(SubTopic subTopic, List<SubTopic> relevantSubTopics, int level = 1)
        {
            SubTopicDetail subTopicDetail = new SubTopicDetail()
                {
                    Level = level,
                    SubTopic = subTopic,
                    Contents = Repository.GetContentsForItem("SubTopic", subTopic.Id)
                };

            IEnumerable<SubTopic> subTopics = Repository.GetSubTopicsForItem("SubTopic", subTopic.Id);
            if (relevantSubTopics != null && relevantSubTopics.Count > 0)
            {
                subTopics = subTopics.Where(x => relevantSubTopics.FirstOrDefault(y => y.Id == x.Id) != null);
            }
            subTopicDetail.SubTopics = subTopics.Select(x => new SubTopicDetail()
            {
                Level = level,
                SubTopic = x,
                Contents = Repository.GetContentsForItem("SubTopic", x.Id)
            });

            foreach (var child in subTopicDetail.SubTopics)
            {
                RecursivelyBuildSubTopicsForSubTopics(child, level + 1, relevantSubTopics);
            }

            return subTopicDetail;
        }

        public IEnumerable<TopicDetail> BuildTopicDetails(List<Topic> list)
        {
            return list.Select(x => new TopicDetail()
                {
                    Topic = x,
                    SubTopics = BuildSubTopicsForTopic(x, null).ToList()
                });
        }

        public TopicDetail BuildTopicDetails(Topic topic, List<SubTopic> relevantSubTopics)
        {
            return new TopicDetail()
            {
                Topic = topic,
                SubTopics = BuildSubTopicsForTopic(topic, relevantSubTopics).ToList(),
                ShowTableOfContents = ShowTableOfContents()
            };
        }

        public bool ShowTableOfContents()
        {
            string showTOC = ConfigurationManager.AppSettings["ShowTOC"];
            if (string.IsNullOrWhiteSpace(showTOC))
            {
                return true;
            }

            return (showTOC.Equals("true", StringComparison.CurrentCultureIgnoreCase));
        }
    }
}