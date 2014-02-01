using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ClinicalKnowledgeManager.ViewModels;
using ClinicalKnowledgeManager.Models;
using ClinicalKnowledgeManager.DB;

namespace ClinicalKnowledgeManager.Helpers
{
    public class ViewModelFactory
    {
        private ModelContext Context = null;

        public ViewModelFactory(ModelContext context)
        {
            Context = context;
        }

        public IEnumerable<SubTopicDetail> BuildSubTopicsForTopic(Topic topic, List<SubTopic> relevantSubTopics)
        {
            IEnumerable<SubTopic> subTopics = Context.SubTopics.Where(x => x.ParentType == "Topic" && x.ParentId == topic.Id).ToList();
            if (relevantSubTopics != null && relevantSubTopics.Count > 0)
            {
                subTopics = subTopics.Where(x => relevantSubTopics.FirstOrDefault(y => y.Id == x.Id) != null);
            }
            IEnumerable<SubTopicDetail> subTopicDetails = subTopics.Select(x => new SubTopicDetail()
            {
                Level = 2,
                SubTopic = x,
                Contents = Context.Contents.Where(c => c.ParentType == "SubTopic" && c.ParentId == x.Id).Select(c => c.Value).ToList()
            }).ToList();
            foreach (var child in subTopicDetails)
            {
                RecursivelyBuildSubTopicsForSubTopics(child, 3, relevantSubTopics);
            }

            return subTopicDetails;
        }

        private void RecursivelyBuildSubTopicsForSubTopics(SubTopicDetail subTopic, int level, List<SubTopic> relevantSubTopics)
        {
            IEnumerable<SubTopic> subTopics = Context.SubTopics.Where(x => x.ParentType == "SubTopic" && x.ParentId == subTopic.SubTopic.Id).ToList();
            if (relevantSubTopics != null && relevantSubTopics.Count > 0)
            {
                subTopics = subTopics.Where(x => relevantSubTopics.FirstOrDefault(y => y.Id == x.Id) != null);
            }
            subTopic.SubTopics = subTopics.Select(x => new SubTopicDetail()
            {
                Level = level,
                SubTopic = x,
                Contents = Context.Contents.Where(c => c.ParentType == "SubTopic" && c.ParentId == x.Id).Select(c => c.Value).ToList()
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
                    Contents = Context.Contents.Where(c => c.ParentType == "SubTopic" && c.ParentId == subTopic.Id).Select(c => c.Value).ToList()
                };

            IEnumerable<SubTopic> subTopics = Context.SubTopics.Where(x => x.ParentType == "SubTopic" && x.ParentId == subTopic.Id).ToList();
            if (relevantSubTopics != null && relevantSubTopics.Count > 0)
            {
                subTopics = subTopics.Where(x => relevantSubTopics.FirstOrDefault(y => y.Id == x.Id) != null);
            }
            subTopicDetail.SubTopics = subTopics.Select(x => new SubTopicDetail()
            {
                Level = level,
                SubTopic = x,
                Contents = Context.Contents.Where(c => c.ParentType == "SubTopic" && c.ParentId == x.Id).Select(c => c.Value).ToList()
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
                SubTopics = BuildSubTopicsForTopic(topic, relevantSubTopics).ToList()
            };
        }
    }
}