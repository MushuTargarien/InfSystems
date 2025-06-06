import { Injectable } from '@nestjs/common';
import { PrismaService } from '../prisma/prisma.service';
import { SendMessageDto } from './dto/send-message.dto';


@Injectable()
export class ChatService {
  constructor(private prisma: PrismaService) {}

  async saveMessage(dto: SendMessageDto) {
    return this.prisma.message.create({
      data: {
        content: dto.content,
        senderId: dto.senderId,
        room: dto.room,
      },
    });
  }

  async getMessages(room?: string) {
  return this.prisma.message.findMany({
    ...(room && { where: { room } }),
  });
}
}