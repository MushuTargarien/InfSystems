import { Controller, Get, Query } from '@nestjs/common';
import { ChatService } from './chat.service';
import { ApiTags, ApiQuery } from '@nestjs/swagger';

@ApiTags('chat')
@Controller('chat')
export class ChatController {
  constructor(private readonly chatService: ChatService) {}

  @Get('history')
  @ApiQuery({ name: 'room', required: false })
  async getHistory(@Query('room') room?: string) {
    return this.chatService.getMessages(room);
  }
}