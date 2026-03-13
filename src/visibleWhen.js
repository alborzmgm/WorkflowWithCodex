const hasValue = (value) => {
  if (Array.isArray(value)) {
    return value.length > 0;
  }

  if (typeof value === 'string') {
    return value.trim().length > 0;
  }

  return value !== null && value !== undefined && value !== false;
};

const isEmpty = (value) => !hasValue(value);

const normalize = (value) => {
  if (Array.isArray(value)) {
    return [...value].sort();
  }

  return value;
};

const evaluateSingleCondition = (rule, values) => {
  const operator = rule.operator ?? 'equals';
  const actual = values[rule.field];
  const expected = rule.value;

  switch (operator) {
    case 'equals':
      return JSON.stringify(normalize(actual)) === JSON.stringify(normalize(expected));
    case 'notEquals':
      return JSON.stringify(normalize(actual)) !== JSON.stringify(normalize(expected));
    case 'contains':
      if (Array.isArray(actual)) {
        return actual.includes(expected);
      }
      if (typeof actual === 'string') {
        return actual.includes(String(expected));
      }
      return false;
    case 'hasValue':
      return hasValue(actual);
    case 'isEmpty':
      return isEmpty(actual);
    default:
      return false;
  }
};

export const evaluateVisibleWhen = (rule, values) => {
  if (!rule) {
    return true;
  }

  if (Array.isArray(rule)) {
    return rule.every((condition) => evaluateVisibleWhen(condition, values));
  }

  if (rule.all) {
    return rule.all.every((condition) => evaluateVisibleWhen(condition, values));
  }

  if (rule.any) {
    return rule.any.some((condition) => evaluateVisibleWhen(condition, values));
  }

  if (rule.not) {
    return !evaluateVisibleWhen(rule.not, values);
  }

  return evaluateSingleCondition(rule, values);
};
