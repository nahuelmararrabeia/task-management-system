"use client";

import { Drawer, Title } from "@task-management/ui";
import { Task } from "../../entities/Task";
import { useMediaQuery } from "../hooks/useMediaQuery";

interface Props {
  task: Task | null;
  onClose: () => void;
}

export function TaskDrawer({ task, onClose }: Props) {
  const isMobile = useMediaQuery("(max-width: 768px)");

  return (
    <Drawer
      open={!!task}
      onClose={onClose}
      side={isMobile ? "bottom" : "right"}
    >
      {task && (
        <div>
            <Title as="h2" size="xl">
              {task.title}
            </Title>
            <div>
              <p>User: {task.assignedUser?.name}</p>
            </div>
            <div>
              <p>Description: {task.description}</p>
            </div>
        </div>
      )}
    </Drawer>
  );
}