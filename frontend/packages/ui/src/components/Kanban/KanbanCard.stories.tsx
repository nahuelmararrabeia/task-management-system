import type { Meta, StoryObj } from "@storybook/react";
import { KanbanCard } from "./KanbanCard";

const meta: Meta<typeof KanbanCard> = {
  title: "Components/Kanban/KanbanCard",
  component: KanbanCard,
};

export default meta;

type Story = StoryObj<typeof KanbanCard>;

export const Default: Story = {
  args: {
    children: "Simple task",
  },
};

export const WithContent: Story = {
  args: {
    children: (
      <div>
        <strong>Task title</strong>
        <p className="text-xs text-gray-500">
          Small description
        </p>
      </div>
    ),
  },
};

export const LongContent: Story = {
  args: {
    children:
      "This is a very long task content to test wrapping and layout behavior inside the card component.",
  },
};