using AutoMapper;
using elearning_platform.DTO;
using elearning_platform.Models;

namespace elearning_platform.Profiles
{
    public class ChatProfile: Profile
    {
        public ChatProfile()
        {
            CreateMap<CreateChatMessageDTO, ChatMessage>();
            CreateMap<UserChat, ReadUserChatDTO>().MaxDepth(5).PreserveReferences();
            CreateMap<Chat, ReadChatDTO>();
            CreateMap<ChatMessage, ReadChatMessageDTO>();
            CreateMap<CreateMessageAttachmentDTO, InternalFileMetadata>();
            CreateMap<CreateChatDTO, Chat>();
            CreateMap<CreateUserChatDTO, UserChat>();
        }
    }
}